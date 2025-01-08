using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Coditech.Admin.Agents
{
    public class MediaManagerFolderAgent : BaseAgent, IMediaManagerFolderAgent
    {
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IMediaManagerClient _mediaManagerClient;
        public MediaManagerFolderAgent(ICoditechLogging coditechLogging, IMediaManagerClient mediaManagerClient)
        {
            _coditechLogging = coditechLogging;
            _mediaManagerClient = GetClient<IMediaManagerClient>(mediaManagerClient);
        }

        public virtual MediaManagerFolderListViewModel GetFolderStructure(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("FileName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("Size", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FileName" : dataTableModel.SortByColumn, dataTableModel.SortBy);
            UserModel userModel = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession);
            dataTableModel.SelectedParameter1 = string.IsNullOrEmpty(dataTableModel.SelectedParameter1) ? "0" : dataTableModel.SelectedParameter1;
            MediaManagerFolderResponse response = _mediaManagerClient.GetFolderStructure(Convert.ToInt32(dataTableModel.SelectedParameter1), userModel.SelectedAdminRoleMasterId, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            MediaManagerFolderListViewModel listViewModel = response.MediaManagerFolderModel.ToViewModel<MediaManagerFolderListViewModel>();
            listViewModel.SelectedParameter1 = listViewModel.ActiveFolderId.ToString();
            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.MediaFiles.Count, BindColumns());
            return listViewModel;
        }

        public virtual MediaModel GetMediaDetails(long mediaId)
        {
            MediaManagerResponse response = _mediaManagerClient.GetMediaDetails(mediaId);
            return response?.MediaModel;
        }

        public virtual FolderListViewModel GetAllFolders(int excludeFolderId)
        {
            FolderListResponse folderListResponse = _mediaManagerClient.GetAllFolders().Result;
            folderListResponse.FolderList.Folders.RemoveAll(x => x.FolderId == excludeFolderId);
            return folderListResponse.FolderList.ToViewModel<FolderListViewModel>();
        }

        public virtual bool MoveFolder(int folderId, int destinationFolderId)
        {
            return _mediaManagerClient.MoveFolderAsync(folderId, destinationFolderId).Result;
        }

        public virtual BooleanModel CreateFolder(int rootFolderId, string folderName)
        {
            int adminRoleMasterId = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession).SelectedAdminRoleMasterId;
            TrueFalseResponse response = _mediaManagerClient.CreateFolderAsync(rootFolderId, folderName, adminRoleMasterId).Result;
            return response.booleanModel;
        }

        public virtual bool DeleteFolder(int folderId)
        {
            return _mediaManagerClient.DeleteFolderAsync(folderId).Result;
        }

        public virtual bool RenameFolder(int folderId, string renameFolderName)
        {
            return _mediaManagerClient.RenameFolderAsync(folderId, renameFolderName).Result;
        }

        public virtual MediaModel UploadFile(int folderId, long mediaId, IFormFile file)
        {
            MediaModel uploadMediaModel = new MediaModel();

            try
            {
                uploadMediaModel.MediaFile = file;
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Info);
                MediaManagerResponse response = _mediaManagerClient.UploadMedia(folderId, string.Empty, mediaId, uploadMediaModel);
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Info);
                if (response.HasError)
                {
                    uploadMediaModel.HasError = response.HasError;
                    uploadMediaModel.ErrorMessage = response.ErrorMessage;
                }
                return uploadMediaModel;
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
                return new MediaModel()
                {
                    ErrorMessage = "Failed to upload a file.",
                    HasError = true,
                };
            }
        }

        public virtual bool DeleteFile(int mediaId)
        {
            return _mediaManagerClient.DeleteFileAsync(mediaId).Result;
        }

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Media",
                ColumnCode = "MediaId",
                IsSortable = false,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "File Name",
                ColumnCode = "FileName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Folder",
                ColumnCode = "FolderName",
                IsSortable = false,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Size",
                ColumnCode = "Size",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
