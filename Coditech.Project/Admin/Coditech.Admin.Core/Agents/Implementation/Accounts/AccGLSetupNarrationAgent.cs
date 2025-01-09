using Coditech.Admin.Utilities;
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
    public class AccGLSetupNarrationAgent : BaseAgent, IAccGLSetupNarrationAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAccGLSetupNarrationClient _accGLSetupNarrationClient;
        #endregion

        #region Public Constructor
        public AccGLSetupNarrationAgent(ICoditechLogging coditechLogging, IAccGLSetupNarrationClient accGLSetupNarrationClient)
        {
            _coditechLogging = coditechLogging;
            _accGLSetupNarrationClient = GetClient<IAccGLSetupNarrationClient>(accGLSetupNarrationClient);
        }
        #endregion

        #region Public Methods
        public virtual AccGLSetupNarrationListViewModel GetNarrationList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("AccGLSetupNarrationId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("NarrationDescription", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("NarrationType", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "AccGLSetupNarrationId" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            AccGLSetupNarrationListResponse response = _accGLSetupNarrationClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            AccGLSetupNarrationListModel deviceList = new AccGLSetupNarrationListModel { AccGLSetupNarrationList= response?.AccGLSetupNarrationList };
            AccGLSetupNarrationListViewModel listViewModel = new AccGLSetupNarrationListViewModel();
            listViewModel.AccGLSetupNarrationList = deviceList?.AccGLSetupNarrationList?.ToViewModel<AccGLSetupNarrationViewModel>().ToList();


            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.AccGLSetupNarrationList.Count, BindColumns());
            return listViewModel;
        }

        //Create AccGLSetupNarration.
        public virtual AccGLSetupNarrationViewModel CreateNarration(AccGLSetupNarrationViewModel accGLSetupNarrationViewModel)
        {
           
            try
            {
                AccGLSetupNarrationResponse response = _accGLSetupNarrationClient.CreateNarration(accGLSetupNarrationViewModel.ToModel<AccGLSetupNarrationModel>());
                AccGLSetupNarrationModel accGLSetupNarrationModel = response?.AccGLSetupNarrationModel;
                return IsNotNull(accGLSetupNarrationModel) ? accGLSetupNarrationModel.ToViewModel<AccGLSetupNarrationViewModel>() : new AccGLSetupNarrationViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (AccGLSetupNarrationViewModel)GetViewModelWithErrorMessage(accGLSetupNarrationViewModel, ex.ErrorMessage);
                    default:
                        return (AccGLSetupNarrationViewModel)GetViewModelWithErrorMessage(accGLSetupNarrationViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Error);
                return (AccGLSetupNarrationViewModel)GetViewModelWithErrorMessage(accGLSetupNarrationViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get AccGLSetupNarration by Narration id.
        public virtual AccGLSetupNarrationViewModel GetNarration(int  accGLSetupNarrationId)
        {
            AccGLSetupNarrationResponse response = _accGLSetupNarrationClient.GetNarration(accGLSetupNarrationId);
            return response?.AccGLSetupNarrationModel.ToViewModel<AccGLSetupNarrationViewModel>();
        }

        //Update AccGLSetupNarration.
        public virtual AccGLSetupNarrationViewModel UpdateNarration(AccGLSetupNarrationViewModel accGLSetupNarrationViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Info);
                AccGLSetupNarrationResponse response = _accGLSetupNarrationClient.UpdateNarration(accGLSetupNarrationViewModel.ToModel<AccGLSetupNarrationModel>());
                AccGLSetupNarrationModel accGLSetupNarrationModel = response?.AccGLSetupNarrationModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Info);
                return IsNotNull(accGLSetupNarrationViewModel) ? accGLSetupNarrationModel.ToViewModel<AccGLSetupNarrationViewModel>() : (AccGLSetupNarrationViewModel)GetViewModelWithErrorMessage(new AccGLSetupNarrationViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Error);
                return (AccGLSetupNarrationViewModel)GetViewModelWithErrorMessage(accGLSetupNarrationViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete AccGLSetupNarration.
        public virtual bool DeleteNarration(string accGLSetupNarrationIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _accGLSetupNarrationClient.DeleteNarration(new ParameterModel { Ids = accGLSetupNarrationIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteAccGLSetupNarrationMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Error);
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
                ColumnName = "Narration ID",
                ColumnCode = "AccGLSetupNarrationId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Narration Type",
                ColumnCode = "NarrationType",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Narration Description",
                ColumnCode = "NarrationDescription",
                IsSortable = false,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "IsActive",
                ColumnCode = "IsActive",
                IsSortable = true,
            });
            return datatableColumnList;
        }

        public virtual AccGLSetupNarrationListResponse GetNarrationList()
        {
            AccGLSetupNarrationListResponse narrationList = _accGLSetupNarrationClient.List(null, null, null, 1, int.MaxValue);
            return narrationList?.AccGLSetupNarrationList?.Count > 0 ? narrationList : new AccGLSetupNarrationListResponse();
        }
        #endregion
    }
}
