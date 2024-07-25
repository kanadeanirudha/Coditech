using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class MediaManagerEndpoint : BaseEndpoint
    {
        public string UploadMediaAsync() =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/UploadMedia";
        public string GetFolderStructureAsync(int rootfolderId) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/FolderStructure?rootFolderId={rootfolderId}";
        public string CreateFolderAsync(int rootFolderId, string folderName) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/CreateFolder?rootFolderId={rootFolderId}&folderName={folderName}";
        public string RenameFolderAsync(int folderId, string renameFolderName) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/RenameFolder?folderId={folderId}&renameFolderName={renameFolderName}";
        public string UploadFileAsync(int folderId) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/UploadFile?folderId={folderId}";
    }
}
