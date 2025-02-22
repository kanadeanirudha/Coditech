using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Microsoft.AspNetCore.Http;

namespace Coditech.Admin.Agents
{
    public interface IMediaManagerFolderAgent
    {
        MediaManagerFolderListViewModel GetFolderStructure(DataTableViewModel dataTableModel = null);
        MediaModel GetMediaDetails(long mediaId);
        BooleanModel CreateFolder(int rootFolderId, string folderName);
        bool RenameFolder(int folderId, string renameFolderName);
        MediaModel UploadFile(int folderId, long mediaId, IFormFile file);
        FolderListViewModel GetMoveFolders(int moveFolderId);
        bool MoveFolder(int folderId, int destinationFolderId);
        bool DeleteFolder(int folderId);
        bool DeleteFile(int mediaId);
    }
}
