using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IMediaManagerClient : IBaseClient
    {
        /// <summary>
        /// Upload Media
        /// </summary>
        /// <returns>UploadMediaModel</returns>
        MediaManagerResponse UploadMedia(int folderId, string folderName, long mediaId, MediaModel model);

        /// <summary>
        /// Get Folder Structure
        /// </summary>
        /// <returns></returns>
        MediaManagerFolderResponse GetFolderStructure(int rootFolderId, int adminRoleId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);
        MediaManagerResponse GetMediaDetails(long mediaId);
        Task<TrueFalseResponse> CreateFolderAsync(int rootFolderId, string folderName, int adminRoleMasterId);
        Task<bool> RenameFolderAsync(int folderId, string renameFolderName);
        Task<FolderListResponse> GetMoveFolders(int moveFolderId);
        Task<bool> MoveFolderAsync(int folderId, int destinationFolderId);
        Task<bool> DeleteFolderAsync(int folderId);
        Task<bool> DeleteFileAsync(int mediaId);
    }
}
