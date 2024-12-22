using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

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

        public MediaManagerFolderListViewModel GetFolderStructure(int rootFolderId = 0, DataTableViewModel dataTableModel = null)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FolderListViewModel GetAllFolders(int excludeFolderId)
        {
            try
            {
                FolderListResponse folderListResponse = _mediaManagerClient.GetAllFolders().Result;
                folderListResponse.FolderList.Folders.RemoveAll(x => x.FolderId == excludeFolderId);
                return folderListResponse.FolderList.ToViewModel<FolderListViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool MoveFolder(int folderId, int destinationFolderId)
        {
            return _mediaManagerClient.MoveFolderAsync(folderId, destinationFolderId).Result;
        }

        public BooleanModel CreateFolder(int rootFolderId, string folderName)
        {
            TrueFalseResponse response = _mediaManagerClient.CreateFolderAsync(rootFolderId, folderName).Result;
            return response.booleanModel;
        }

        public bool DeleteFolder(int folderId)
        {
            return _mediaManagerClient.DeleteFolderAsync(folderId).Result;
        }

        public bool RenameFolder(int folderId, string renameFolderName)
        {
            return _mediaManagerClient.RenameFolderAsync(folderId, renameFolderName).Result;
        }

        public bool UploadFile(int folderId, IFormFile file)
        {
            UploadMediaModel uploadMediaModel = new UploadMediaModel();
            uploadMediaModel.MediaFile = file;
            MediaManagerResponse response = _mediaManagerClient.UploadMedia(folderId, string.Empty, uploadMediaModel);
            return response != null && !response.HasError;
        }

        public bool DeleteFile(int mediaId)
        {
            return _mediaManagerClient.DeleteFileAsync(mediaId).Result;
        }
    }
}
