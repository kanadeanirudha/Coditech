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
        MediaManagerResponse UploadMedia(int folderId, string folderName, UploadMediaModel model);

        /// <summary>
        /// Get Folder Structure
        /// </summary>
        /// <returns></returns>
        Task<MediaManagerFolderResponse> GetFolderStructure(int rootFolderId = 0, int adminRoleId = 0, bool isAdminUser = false, int? pageIndex = 0, int? pageSize = 10);
        Task<TrueFalseResponse> CreateFolderAsync(int rootFolderId, string folderName);
        Task<bool> RenameFolderAsync(int folderId, string renameFolderName);
        Task<FolderListResponse> GetAllFolders();
        Task<bool> MoveFolderAsync(int folderId, int destinationFolderId);
        Task<bool> DeleteFolderAsync(int folderId);
        Task<bool> DeleteFileAsync(int mediaId);
    }
}
