using Coditech.Admin.Controllers;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Responses;

namespace Coditech.Admin.Agents
{
    public interface IMediaManagerFolderAgent
    {
        MediaManagerFolderListViewModel GetFolderStructure(int rootFolderId = 0);
        bool CreateFolder(int rootFolderId, string folderName);
        bool RenameFolder(int folderId, string renameFolderName);
        bool UploadFile(int folderId, IFormFile file);
    }
}
