using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;

namespace Coditech.API.Client
{
    public interface IMediaManagerClient : IBaseClient
    {
        /// <summary>
        /// Upload Media
        /// </summary>
        /// <returns>UploadMediaModel</returns>
        MediaManagerResponse UploadMedia(UploadMediaModel model);

        /// <summary>
        /// Get Folder Structure
        /// </summary>
        /// <returns></returns>
        Task<MediaManagerFolderResponse> GetFolderStructure(int rootFolderId = 0);
        Task<TrueFalseResponse> CreateFolderAsync(int rootFolderId, string folderName);
        Task<bool> RenameFolderAsync(int folderId, string renameFolderName);
        Task<TrueFalseResponse> UploadFileAsync(int folderId, UploadMediaModel body);
        Task<FolderListResponse> GetAllFolders();
        Task<bool> MoveFolderAsync(int folderId, int destinationFolderId);
        Task<bool> DeleteFolderAsync(int folderId);
        Task<bool> DeleteFileAsync(int mediaId);
    }
}
