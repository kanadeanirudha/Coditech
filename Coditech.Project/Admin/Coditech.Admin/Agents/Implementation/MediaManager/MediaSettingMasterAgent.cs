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
                filters.Add("MediaTypeMasterId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MediaConfigurationId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MaxSizeInMB", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MediaTypeExtensionMasterIds", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LargeImageResize", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MediumImageResize", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("SmallImageResize", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CrossSellImageResize", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("SmallThumbnailImageResize", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("HelpDescription", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "MediaSettingMasterName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            MediaSettingMasterListResponse response = _mediaSettingMasterClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            MediaSettingMasterListModel mediaSettingMasterList = new MediaSettingMasterListModel { MediaSettingMasterList = response?.MediaSettingMasterList };
            MediaSettingMasterListViewModel listViewModel = new MediaSettingMasterListViewModel();
            listViewModel.MediaSettingMasterList = mediaSettingMasterList?.MediaSettingMasterList?.ToViewModel<MediaSettingMasterViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.MediaSettingMasterList.Count, BindColumns());
            return listViewModel;
        }

        //Create Media Setting Master.
        public virtual MediaSettingMasterViewModel CreateMediaSettingMaster(MediaSettingMasterViewModel mediaSettingMasterViewModel)
        {
            try
            {
                MediaSettingMasterResponse response = _mediaSettingMasterClient.CreateMediaSettingMaster(mediaSettingMasterViewModel.ToModel<MediaSettingMasterModel>());
                MediaSettingMasterModel mediaSettingMasterModel = response?.MediaSettingMasterModel;
                return IsNotNull(mediaSettingMasterModel) ? mediaSettingMasterModel.ToViewModel<MediaSettingMasterViewModel>() : new MediaSettingMasterViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (MediaSettingMasterViewModel)GetViewModelWithErrorMessage(mediaSettingMasterViewModel, ex.ErrorMessage);
                    default:
                        return (MediaSettingMasterViewModel)GetViewModelWithErrorMessage(mediaSettingMasterViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaSettingMaster.ToString(), TraceLevel.Error);
                return (MediaSettingMasterViewModel)GetViewModelWithErrorMessage(mediaSettingMasterViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get media setting master by media setting master id.
        public virtual MediaSettingMasterViewModel GetMediaSettingMaster(short mediaSettingMasterId)
        {
            MediaSettingMasterResponse response = _mediaSettingMasterClient.GetMediaSettingMaster(mediaSettingMasterId);
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
                ColumnCode = "MediaTypeMasterId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Media Configuration",
                ColumnCode = "MediaConfigurationId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Max Size",
                ColumnCode = "MaxSizeInMB",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Media Type Extension",
                ColumnCode = "MediaTypeExtensionMasterIds",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Large Image",
                ColumnCode = "LargeImageResize",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Medium Image",
                ColumnCode = "MediumImageResize",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Small Image",
                ColumnCode = "SmallImageResize",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "CrossSell Image",
                ColumnCode = "CrossSellImageResize",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Thumbnail Image",
                ColumnCode = "ThumbnailImageResize",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Small Thumbnail Image",
                ColumnCode = "SmallThumbnailImageResize",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Help Description",
                ColumnCode = "HelpDescription",
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
