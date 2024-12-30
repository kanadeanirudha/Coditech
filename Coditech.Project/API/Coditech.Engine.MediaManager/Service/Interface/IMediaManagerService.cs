using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IMediaManagerService
    {
        MediaManagerResponse UploadMedia(int folderId, string folderName, IEnumerable<IFormFile> files, HttpRequest request);
        MediaManagerFolderResponse GetMediaList(int rootFolderId, int adminRoleId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        bool PostRenameFolder(int FolderId, string RenameFolderName);
        TrueFalseResponse PostCreateFolder(int RootFolderId, string FolderName, int adminRoleMasterId);
        FolderListResponse GetAllFolders();
        bool MoveFolder(int folderId, int destinationFolderId);
        bool DeleteFolder(int folderId);
        bool DeleteFile(int mediaId);
    }
}
