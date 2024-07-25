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
    public class MediaSettingMasterAgent : BaseAgent, IMediaSettingMasterAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IMediaSettingMasterClient _mediaSettingMasterClient;
        #endregion

        #region Public Constructor
        public MediaSettingMasterAgent(ICoditechLogging coditechLogging, IMediaSettingMasterClient mediaSettingMasterClient)
        {
            _coditechLogging = coditechLogging;
            _mediaSettingMasterClient = GetClient<IMediaSettingMasterClient>(mediaSettingMasterClient);
        }
        #endregion

        #region Public Methods
        public virtual MediaSettingMasterListViewModel GetMediaSettingMasterList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("MediaType", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("IsActive", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            MediaSettingMasterListResponse response = _mediaSettingMasterClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            MediaSettingMasterListModel mediaSettingMasterList = new MediaSettingMasterListModel { MediaSettingMasterList = response?.MediaSettingMasterList };
            MediaSettingMasterListViewModel listViewModel = new MediaSettingMasterListViewModel();
            listViewModel.MediaSettingMasterList = mediaSettingMasterList?.MediaSettingMasterList?.ToViewModel<MediaSettingMasterViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.MediaSettingMasterList.Count, BindColumns());
            return listViewModel;
        }

        //Get media setting master by mediaTypeMasterId.
        public virtual MediaSettingMasterViewModel GetMediaSettingMaster(byte mediaTypeMasterId)
        {
            MediaSettingMasterResponse response = _mediaSettingMasterClient.GetMediaSettingMaster(mediaTypeMasterId);
            return response?.MediaSettingMasterModel.ToViewModel<MediaSettingMasterViewModel>();
        }

        //Update mediaSettingMaster.
        public virtual MediaSettingMasterViewModel UpdateMediaSettingMaster(MediaSettingMasterViewModel mediaSettingMasterViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Info);
                MediaSettingMasterResponse response = _mediaSettingMasterClient.UpdateMediaSettingMaster(mediaSettingMasterViewModel.ToModel<MediaSettingMasterModel>());
                MediaSettingMasterModel mediaSettingMasterModel = response?.MediaSettingMasterModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Info);
                return IsNotNull(mediaSettingMasterModel) ? mediaSettingMasterModel.ToViewModel<MediaSettingMasterViewModel>() : (MediaSettingMasterViewModel)GetViewModelWithErrorMessage(new MediaSettingMasterViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Error);
                return (MediaSettingMasterViewModel)GetViewModelWithErrorMessage(mediaSettingMasterViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete mediaSettingMaster.
        public virtual bool DeleteMediaSettingMaster(string mediaSettingMasterId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _mediaSettingMasterClient.DeleteMediaSettingMaster(new ParameterModel { Ids = mediaSettingMasterId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteMediaSettingMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Media Type",
                ColumnCode = "MediaType",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "IsActive",
                ColumnCode = "IsActive",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all media setting master list from database 
        public virtual MediaSettingMasterListResponse GetMediaSettingMasterList()
        {
            MediaSettingMasterListResponse mediaSettingMasterList = _mediaSettingMasterClient.List(null, null, null, 1, int.MaxValue);
            return mediaSettingMasterList?.MediaSettingMasterList?.Count > 0 ? mediaSettingMasterList : new MediaSettingMasterListResponse();
        }
        #endregion
    }
}
