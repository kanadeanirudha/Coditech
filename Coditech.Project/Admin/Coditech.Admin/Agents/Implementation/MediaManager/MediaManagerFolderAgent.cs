using Coditech.Admin.Controllers;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.API.Model;
using Coditech.API.Client;
using Coditech.Common.Logger;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;

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

        public bool CreateFolder(int rootFolderId, string folderName)
        {
            return _mediaManagerClient.CreateFolderAsync(rootFolderId, folderName).Result;
        }

        public bool RenameFolder(int folderId, string renameFolderName)
        {
            return _mediaManagerClient.RenameFolderAsync(folderId, renameFolderName).Result;
        }

        public bool UploadFile(int folderId, IFormFile file)
        {
            UploadMediaModel uploadMediaModel = new UploadMediaModel();
            uploadMediaModel.MediaFile = file;
            return _mediaManagerClient.UploadFileAsync(folderId, uploadMediaModel).Result;
        }
    }
}
