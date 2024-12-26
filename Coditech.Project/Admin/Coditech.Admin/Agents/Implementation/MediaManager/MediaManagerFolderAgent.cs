using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
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

        public virtual MediaManagerFolderListViewModel GetFolderStructure(int rootFolderId = 0, DataTableViewModel dataTableModel = null)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            UserModel userModel = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession);
            MediaManagerFolderResponse mediaManagerFolderResponse = _mediaManagerClient.GetFolderStructure(rootFolderId, userModel.SelectedAdminRoleMasterId, userModel.IsAdminUser, dataTableModel.PageIndex, dataTableModel.PageSize).Result;


            var result = mediaManagerFolderResponse.MediaManagerFolderModel.ToViewModel<MediaManagerFolderListViewModel>();

            result.PageListViewModel.Page = Convert.ToInt32(mediaManagerFolderResponse.MediaManagerFolderModel.PageIndex);
            result.PageListViewModel.RecordPerPage = Convert.ToInt32(mediaManagerFolderResponse.MediaManagerFolderModel.PageSize);
            result.PageListViewModel.TotalPages = (int)Math.Ceiling((decimal)((double)mediaManagerFolderResponse.MediaManagerFolderModel.TotalCount / mediaManagerFolderResponse.MediaManagerFolderModel.PageSize));
            result.PageListViewModel.TotalResults = Convert.ToInt32(mediaManagerFolderResponse.MediaManagerFolderModel.TotalCount);
            result.PageListViewModel.TotalRecordCount = Convert.ToInt32(mediaManagerFolderResponse.MediaManagerFolderModel.MediaFiles.Count());
            result.PageListViewModel.SearchBy = dataTableModel.SearchBy ?? string.Empty;
            result.PageListViewModel.SortByColumn = dataTableModel.SortByColumn ?? string.Empty;
            result.PageListViewModel.SortBy = dataTableModel.SortBy ?? string.Empty;

            return result;
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
            TrueFalseResponse response = _mediaManagerClient.CreateFolderAsync(rootFolderId, folderName).Result;
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

        public virtual UploadMediaModel UploadFile(int folderId, IFormFile file)
        {
            UploadMediaModel uploadMediaModel = new UploadMediaModel();

            try
            {
                uploadMediaModel.MediaFile = file;
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Info);
                MediaManagerResponse response = _mediaManagerClient.UploadMedia(folderId, string.Empty, uploadMediaModel);
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
                return new UploadMediaModel()
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
    }
}
