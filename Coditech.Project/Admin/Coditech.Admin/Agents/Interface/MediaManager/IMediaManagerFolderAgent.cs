using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IMediaManagerFolderAgent
    {
        MediaManagerFolderListViewModel GetFolderStructure(int rootFolderId = 0);
        BooleanModel CreateFolder(int rootFolderId, string folderName);
        bool RenameFolder(int folderId, string renameFolderName);
        BooleanModel UploadFile(int folderId, IFormFile file);
        FolderListViewModel GetAllFolders(int excludeFolderId);
        bool MoveFolder(int folderId, int destinationFolderId);
        bool DeleteFolder(int folderId);
        bool DeleteFile(int mediaId);
    }
}
