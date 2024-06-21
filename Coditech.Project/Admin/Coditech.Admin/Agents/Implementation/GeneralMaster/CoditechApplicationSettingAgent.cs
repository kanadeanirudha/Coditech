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
    public class CoditechApplicationSettingAgent : BaseAgent, ICoditechApplicationSettingAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechApplicationSettingClient _coditechApplicationSettingClient;
        #endregion

        #region Public Constructor
        public CoditechApplicationSettingAgent(ICoditechLogging coditechLogging, ICoditechApplicationSettingClient coditechApplicationSettingClient)
        {
            _coditechLogging = coditechLogging;
            _coditechApplicationSettingClient = GetClient<ICoditechApplicationSettingClient>(coditechApplicationSettingClient);
        }
        #endregion

        #region Public Methods
        public virtual CoditechApplicationSettingListViewModel GetCoditechApplicationSettingList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("ApplicationCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ApplicationValue1", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            CoditechApplicationSettingListResponse response = _coditechApplicationSettingClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            CoditechApplicationSettingListModel coditechApplicationSettingList = new CoditechApplicationSettingListModel { CoditechApplicationSettingList = response?.CoditechApplicationSettingList };
            CoditechApplicationSettingListViewModel listViewModel = new CoditechApplicationSettingListViewModel();
            listViewModel.CoditechApplicationSettingList = coditechApplicationSettingList?.CoditechApplicationSettingList?.ToViewModel<CoditechApplicationSettingViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.CoditechApplicationSettingList.Count, BindColumns());
            return listViewModel;
        }

        //Create Coditech Application Setting.
        public virtual CoditechApplicationSettingViewModel CreateCoditechApplicationSetting(CoditechApplicationSettingViewModel coditechApplicationSettingViewModel)
        {
            try
            {
                CoditechApplicationSettingResponse response = _coditechApplicationSettingClient.CreateCoditechApplicationSetting(coditechApplicationSettingViewModel.ToModel<CoditechApplicationSettingModel>());
                CoditechApplicationSettingModel coditechApplicationSettingModel = response?.CoditechApplicationSettingModel;
                return IsNotNull(coditechApplicationSettingModel) ? coditechApplicationSettingModel.ToViewModel<CoditechApplicationSettingViewModel>() : new CoditechApplicationSettingViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (CoditechApplicationSettingViewModel)GetViewModelWithErrorMessage(coditechApplicationSettingViewModel, ex.ErrorMessage);
                    default:
                        return (CoditechApplicationSettingViewModel)GetViewModelWithErrorMessage(coditechApplicationSettingViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Error);
                return (CoditechApplicationSettingViewModel)GetViewModelWithErrorMessage(coditechApplicationSettingViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Coditech Application Setting by coditechApplicationSettingId.
        public virtual CoditechApplicationSettingViewModel GetCoditechApplicationSetting(short coditechApplicationSettingId)
        {
            CoditechApplicationSettingResponse response = _coditechApplicationSettingClient.GetCoditechApplicationSetting(coditechApplicationSettingId);
            return response?.CoditechApplicationSettingModel.ToViewModel<CoditechApplicationSettingViewModel>();
        }

        //Update CoditechApplicationSetting.
        public virtual CoditechApplicationSettingViewModel UpdateCoditechApplicationSetting(CoditechApplicationSettingViewModel coditechApplicationSettingViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Info);
                CoditechApplicationSettingResponse response = _coditechApplicationSettingClient.UpdateCoditechApplicationSetting(coditechApplicationSettingViewModel.ToModel<CoditechApplicationSettingModel>());
                CoditechApplicationSettingModel coditechApplicationSettingModel = response?.CoditechApplicationSettingModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Info);
                return IsNotNull(coditechApplicationSettingModel) ? coditechApplicationSettingModel.ToViewModel<CoditechApplicationSettingViewModel>() : (CoditechApplicationSettingViewModel)GetViewModelWithErrorMessage(new CoditechApplicationSettingViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Error);
                return (CoditechApplicationSettingViewModel)GetViewModelWithErrorMessage(coditechApplicationSettingViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete coditechApplicationSetting.
        public virtual bool DeleteCoditechApplicationSetting(string coditechApplicationSettingIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _coditechApplicationSettingClient.DeleteCoditechApplicationSetting(new ParameterModel { Ids = coditechApplicationSettingIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteCoditechApplicationSetting;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechApplicationSetting.ToString(), TraceLevel.Error);
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
                ColumnName = "Application Code",
                ColumnCode = "ApplicationCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Application Value 1",
                ColumnCode = "ApplicationValue1",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Application Value 2",
                ColumnCode = "ApplicationValue2",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Application Value 3",
                ColumnCode = "ApplicationValue3",
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all CoditechApplicationSetting list from database 
        public virtual CoditechApplicationSettingListResponse GetCoditechApplicationSettingList()
        {
            CoditechApplicationSettingListResponse coditechApplicationSettingList = _coditechApplicationSettingClient.List(null, null, null, 1, int.MaxValue);
            return coditechApplicationSettingList?.CoditechApplicationSettingList?.Count > 0 ? coditechApplicationSettingList : new CoditechApplicationSettingListResponse();
        }
        #endregion
    }
}
