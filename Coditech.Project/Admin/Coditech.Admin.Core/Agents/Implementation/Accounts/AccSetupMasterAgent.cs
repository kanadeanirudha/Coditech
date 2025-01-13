using Coditech.Admin.ViewModel;
using Coditech.API.Client;
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
    public class AccSetupMasterAgent : BaseAgent, IAccSetupMasterAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAccSetupMasterClient _accSetupMasterClient;
        #endregion

        #region Public Constructor
        public AccSetupMasterAgent(ICoditechLogging coditechLogging, IAccSetupMasterClient accSetupMasterClient)
        {
            _coditechLogging = coditechLogging;
            _accSetupMasterClient = GetClient<IAccSetupMasterClient>(accSetupMasterClient);
        }
        #endregion

        #region Public Methods
        
        public virtual AccSetupMasterListViewModel GetAccSetupMasterList (DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            filters.Add(FilterKeys.SelectedCentreCode, ProcedureFilterOperators.Equals, dataTableModel.SelectedCentreCode);
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("FiscalYearDay", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("FiscalYearMonth", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            AccSetupMasterListResponse response = _accSetupMasterClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            AccSetupMasterListModel MasterList = new AccSetupMasterListModel { AccSetupMasterList = response?.AccSetupMasterList };
            AccSetupMasterListViewModel listViewModel = new AccSetupMasterListViewModel();
            listViewModel.AccSetupMasterList = MasterList?.AccSetupMasterList?.ToViewModel<AccSetupMasterViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.AccSetupMasterList.Count, BindColumns());
            return listViewModel;
        }


        //Create  Fiscal Year.
        public virtual AccSetupMasterViewModel CreateAccSetupMaster(AccSetupMasterViewModel accSetupMasterViewModel)
        {
            try
            {
                AccSetupMasterResponse response = _accSetupMasterClient.CreateAccSetupMaster(accSetupMasterViewModel.ToModel<AccSetupMasterModel>());
                AccSetupMasterModel accSetupMasterModel = response?.AccSetupMasterModel;
                return IsNotNull(accSetupMasterModel) ? accSetupMasterModel.ToViewModel<AccSetupMasterViewModel>() : new AccSetupMasterViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (AccSetupMasterViewModel)GetViewModelWithErrorMessage(accSetupMasterViewModel, ex.ErrorMessage);
                    default:
                        return (AccSetupMasterViewModel)GetViewModelWithErrorMessage(accSetupMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Error);
                return (AccSetupMasterViewModel)GetViewModelWithErrorMessage(accSetupMasterViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get  AccSetupMaster by  AccSetupMaster  id.
        public virtual AccSetupMasterViewModel GetAccSetupMaster(short accSetupMasterId)
        {
            AccSetupMasterResponse response = _accSetupMasterClient.GetAccSetupMaster(accSetupMasterId);
            return response?.AccSetupMasterModel.ToViewModel<AccSetupMasterViewModel>();
        }

        //Update AccSetupMaster.
        public virtual AccSetupMasterViewModel UpdateAccSetupMaster(AccSetupMasterViewModel accSetupMasterViewModel)
        {
            try
            {

                AccSetupMasterResponse response = _accSetupMasterClient.UpdateAccSetupMaster(accSetupMasterViewModel.ToModel<AccSetupMasterModel>());
                AccSetupMasterModel accSetupMasterModel = response?.AccSetupMasterModel;
                return IsNotNull(accSetupMasterModel) ? accSetupMasterModel.ToViewModel<AccSetupMasterViewModel>() : new AccSetupMasterViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster .ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (AccSetupMasterViewModel)GetViewModelWithErrorMessage(accSetupMasterViewModel, ex.ErrorMessage);
                    default:
                        return (AccSetupMasterViewModel)GetViewModelWithErrorMessage(accSetupMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Error);
                return (AccSetupMasterViewModel)GetViewModelWithErrorMessage(accSetupMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete accSetupMaster.
        public virtual bool DeleteAccSetupMaster(string accSetupMasterId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _accSetupMasterClient.DeleteAccSetupMaster(new ParameterModel { Ids = accSetupMasterId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteAccSetupMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Fiscal Year Day",
                ColumnCode = "FiscalYearDay",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Fiscal Year Month",
                ColumnCode = "FiscalYearMonth",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Active",
                ColumnCode = "IsActive",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all FiscalYear list from database 
        public virtual AccSetupMasterListResponse GetAccSetupMasterList()
        {
            AccSetupMasterListResponse AccMasterList = _accSetupMasterClient.List(null, null, null, 1, int.MaxValue);
            return AccMasterList?.AccSetupMasterList?.Count > 0 ? AccMasterList : new AccSetupMasterListResponse();
        }
        #endregion
    }
}
