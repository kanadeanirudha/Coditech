using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IMediaManagerFolderAgent
    {
        MediaManagerFolderListViewModel GetFolderStructure(int rootFolderId = 0);
        bool CreateFolder(int rootFolderId, string folderName);
        bool RenameFolder(int folderId, string renameFolderName);
        bool UploadFile(int folderId, IFormFile file);
        FolderListViewModel GetAllFolders(int excludeFolderId);
        bool MoveFolder(int folderId, int destinationFolderId);
        bool DeleteFolder(int folderId);
    }
}
