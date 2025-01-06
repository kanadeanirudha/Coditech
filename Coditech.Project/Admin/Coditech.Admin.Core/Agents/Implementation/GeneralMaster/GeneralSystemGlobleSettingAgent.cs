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
    public class GeneralSystemGlobleSettingAgent : BaseAgent, IGeneralSystemGlobleSettingAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralSystemGlobleSettingClient _generalSystemGlobleSettingClient;
        #endregion

        #region Public Constructor
        public GeneralSystemGlobleSettingAgent(ICoditechLogging coditechLogging, IGeneralSystemGlobleSettingClient generalSystemGlobleSettingClient)
        {
            _coditechLogging = coditechLogging;
            _generalSystemGlobleSettingClient = GetClient<IGeneralSystemGlobleSettingClient>(generalSystemGlobleSettingClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralSystemGlobleSettingListViewModel GetSystemGlobleSettingList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("FeatureName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("FeatureValue", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FeatureName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralSystemGlobleSettingListResponse response = _generalSystemGlobleSettingClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralSystemGlobleSettingListModel systemGlobleSettingList = new GeneralSystemGlobleSettingListModel { GeneralSystemGlobleSettingList = response?.GeneralSystemGlobleSettingList };
            GeneralSystemGlobleSettingListViewModel listViewModel = new GeneralSystemGlobleSettingListViewModel();
            listViewModel.GeneralSystemGlobleSettingList = systemGlobleSettingList?.GeneralSystemGlobleSettingList?.ToViewModel<GeneralSystemGlobleSettingViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralSystemGlobleSettingList.Count, BindColumns());
            return listViewModel;
        }

        //Create General System Globle Setting.
        public virtual GeneralSystemGlobleSettingViewModel CreateSystemGlobleSetting(GeneralSystemGlobleSettingViewModel generalSystemGlobleSettingViewModel)
        {
            try
            {
                GeneralSystemGlobleSettingResponse response = _generalSystemGlobleSettingClient.CreateSystemGlobleSetting(generalSystemGlobleSettingViewModel.ToModel<GeneralSystemGlobleSettingModel>());
                GeneralSystemGlobleSettingModel generalSystemGlobleSettingModel = response?.GeneralSystemGlobleSettingModel;
                return IsNotNull(generalSystemGlobleSettingModel) ? generalSystemGlobleSettingModel.ToViewModel<GeneralSystemGlobleSettingViewModel>() : new GeneralSystemGlobleSettingViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralSystemGlobleSettingViewModel)GetViewModelWithErrorMessage(generalSystemGlobleSettingViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralSystemGlobleSettingViewModel)GetViewModelWithErrorMessage(generalSystemGlobleSettingViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Error);
                return (GeneralSystemGlobleSettingViewModel)GetViewModelWithErrorMessage(generalSystemGlobleSettingViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get General System Globle Setting by generalSystemGlobleSettingId.
        public virtual GeneralSystemGlobleSettingViewModel GetSystemGlobleSetting(short generalSystemGlobleSettingId)
        {
            GeneralSystemGlobleSettingResponse response = _generalSystemGlobleSettingClient.GetSystemGlobleSetting(generalSystemGlobleSettingId);
            return response?.GeneralSystemGlobleSettingModel.ToViewModel<GeneralSystemGlobleSettingViewModel>();
        }

        //Update generalSystemGlobleSetting.
        public virtual GeneralSystemGlobleSettingViewModel UpdateSystemGlobleSetting(GeneralSystemGlobleSettingViewModel generalSystemGlobleSettingViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Info);
                GeneralSystemGlobleSettingResponse response = _generalSystemGlobleSettingClient.UpdateSystemGlobleSetting(generalSystemGlobleSettingViewModel.ToModel<GeneralSystemGlobleSettingModel>());
                GeneralSystemGlobleSettingModel generalSystemGlobleSettingModel = response?.GeneralSystemGlobleSettingModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalSystemGlobleSettingModel) ? generalSystemGlobleSettingModel.ToViewModel<GeneralSystemGlobleSettingViewModel>() : (GeneralSystemGlobleSettingViewModel)GetViewModelWithErrorMessage(new GeneralSystemGlobleSettingViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Error);
                return (GeneralSystemGlobleSettingViewModel)GetViewModelWithErrorMessage(generalSystemGlobleSettingViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalSystemGlobleSetting.
        public virtual bool DeleteSystemGlobleSetting(string generalSystemGlobleSettingId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalSystemGlobleSettingClient.DeleteSystemGlobleSetting(new ParameterModel { Ids = generalSystemGlobleSettingId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteSystemGlobleSettingMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GlobleSettingMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Feature Name",
                ColumnCode = "FeatureName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Feature Default Value",
                ColumnCode = "FeatureDefaultValue",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Feature Value",
                ColumnCode = "FeatureValue",
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all SystemGlobleSetting list from database 
        public virtual GeneralSystemGlobleSettingListResponse GetSystemGlobleSettingList()
        {
            GeneralSystemGlobleSettingListResponse systemGlobleSettingList = _generalSystemGlobleSettingClient.List(null, null, null, 1, int.MaxValue);
            return systemGlobleSettingList?.GeneralSystemGlobleSettingList?.Count > 0 ? systemGlobleSettingList : new GeneralSystemGlobleSettingListResponse();
        }
        #endregion
    }
}
