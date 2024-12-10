using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class TicketMasterAgent : BaseAgent, ITicketMasterAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ITicketMasterClient _ticketMasterClient;
        #endregion

        #region Public Constructor
        public TicketMasterAgent(ICoditechLogging coditechLogging, ITicketMasterClient ticketMasterClient)
        {
            _coditechLogging = coditechLogging;
            _ticketMasterClient = GetClient<ITicketMasterClient>(ticketMasterClient);
        }
        #endregion

        #region Public Methods
        public virtual TicketMasterListViewModel GetTicketMasterList(DataTableViewModel dataTableModel)
        {
            long userMasterId = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.UserMasterId ?? 0;
            if (userMasterId > 0)
            {
                FilterCollection filters = new FilterCollection();
                dataTableModel = dataTableModel ?? new DataTableViewModel();
                if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
                {
                    filters.Add("TicketNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                    filters.Add("TicketStatusEnumId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                    filters.Add("CreatedDate", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                }
                SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

                TicketMasterListResponse response = _ticketMasterClient.List(userMasterId, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
                TicketMasterListModel ticketMasterList = new TicketMasterListModel { TicketMasterList = response?.TicketMasterList };
                TicketMasterListViewModel listViewModel = new TicketMasterListViewModel();
                listViewModel.TicketMasterList = ticketMasterList?.TicketMasterList?.ToViewModel<TicketMasterViewModel>().ToList();

                SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.TicketMasterList.Count, BindColumns());
                listViewModel.UserId = userMasterId;
                return listViewModel;
            }
            return new TicketMasterListViewModel();
        }

        //Create TicketMaster.
        public virtual TicketMasterViewModel CreateTicket(TicketMasterViewModel ticketMasterViewModel)
        {
            try
            {
                TicketMasterResponse response = _ticketMasterClient.CreateTicket(ticketMasterViewModel.ToModel<TicketMasterModel>());
                TicketMasterModel ticketMasterModel = response?.TicketMasterModel;
                return IsNotNull(ticketMasterModel) ? ticketMasterModel.ToViewModel<TicketMasterViewModel>() : new TicketMasterViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (TicketMasterViewModel)GetViewModelWithErrorMessage(ticketMasterViewModel, ex.ErrorMessage);
                    default:
                        return (TicketMasterViewModel)GetViewModelWithErrorMessage(ticketMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Error);
                return (TicketMasterViewModel)GetViewModelWithErrorMessage(ticketMasterViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //GetTicketMaster by ticketMaster id.
        public virtual TicketMasterViewModel GetTicket(long ticketMasterId,long userMasterId)
        {
            TicketMasterResponse response = _ticketMasterClient.GetTicket(ticketMasterId, userMasterId);
            return response?.TicketMasterModel.ToViewModel<TicketMasterViewModel>();
        }

        //Update TicketMaster.
        public virtual TicketMasterViewModel UpdateTicket(TicketMasterViewModel ticketMasterViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Info);
                TicketMasterResponse response = _ticketMasterClient.UpdateTicket(ticketMasterViewModel.ToModel<TicketMasterModel>());
                TicketMasterModel ticketMasterModel = response?.TicketMasterModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Info);
                return IsNotNull(ticketMasterModel) ? ticketMasterModel.ToViewModel<TicketMasterViewModel>() : (TicketMasterViewModel)GetViewModelWithErrorMessage(new TicketMasterViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Error);
                return (TicketMasterViewModel)GetViewModelWithErrorMessage(ticketMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete TicketMaster.
        public virtual bool DeleteTicket(string ticketMasterIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _ticketMasterClient.DeleteTicket(new ParameterModel { Ids = ticketMasterIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteTicketMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();           
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Ticket Number",
                ColumnCode = "TicketNumber",
                IsSortable = true,
            });            
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = " Ticket Status",
                ColumnCode = "TicketStatusEnumId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Created Date",
                ColumnCode = "CreatedDate",
                IsSortable = true,
            });           
            return datatableColumnList;
        }
        #endregion
    }
}
