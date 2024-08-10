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

        public MediaManagerFolderListViewModel GetFolderStructure(int rootFolderId = 0)
        {
            try
            {
                MediaManagerFolderResponse mediaManagerFolderResponse = _mediaManagerClient.GetFolderStructure(rootFolderId).Result;
                return mediaManagerFolderResponse.MediaManagerFolderModel.ToViewModel<MediaManagerFolderListViewModel>();
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

        public BooleanModel UploadFile(int folderId, IFormFile file)
        {
            UploadMediaModel uploadMediaModel = new UploadMediaModel();
            uploadMediaModel.MediaFile = file;
            TrueFalseResponse response = _mediaManagerClient.UploadFileAsync(folderId, uploadMediaModel).Result;
            return response.booleanModel; 
        }

        public bool DeleteFile(int mediaId)
        {
            return _mediaManagerClient.DeleteFileAsync(mediaId).Result;
        }
    }
}
