using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;

namespace Coditech.API.Service
{
    public interface IMediaManagerService
    {
        MediaManagerResponse UploadMedia(int folderId, string folderName, IEnumerable<IFormFile> files, HttpRequest request);
        MediaManagerFolderResponse GetFolderStructure(int rootFolderId = 0, int adminRoleId = 0, bool isAdminUser = false, int pageIndex = 0, int pageSize = 0);
        bool PostRenameFolder(int FolderId, string RenameFolderName);
        TrueFalseResponse PostCreateFolder(int RootFolderId, string FolderName);
        FolderListResponse GetAllFolders();
        bool MoveFolder(int folderId, int destinationFolderId);
        bool DeleteFolder(int folderId);
        bool DeleteFile(int mediaId);
    }
}
