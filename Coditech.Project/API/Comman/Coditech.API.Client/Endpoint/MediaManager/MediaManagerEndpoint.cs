using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class MediaManagerEndpoint : BaseEndpoint
    {
        public string UploadMediaAsync(int folderId,string folderName) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/UploadMedia?folderId={folderId}&folderName={folderName}";
        public string GetFolderStructureAsync(int rootfolderId, int adminRoleId, bool isAdminUser, int? pageIndex, int? pageSize) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/FolderStructure?rootFolderId={rootfolderId}&adminRoleId={adminRoleId}&isAdminUser={isAdminUser}{BuildEndpointQueryString(true, null, null, null, pageIndex, pageSize)}";
        public string CreateFolderAsync(int rootFolderId, string folderName) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/CreateFolder?rootFolderId={rootFolderId}&folderName={folderName}";
        public string RenameFolderAsync(int folderId, string renameFolderName) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/RenameFolder?folderId={folderId}&renameFolderName={renameFolderName}";
        public string GetAllFolders() =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/GetAllFolders";
        public string MoveFolderAsync(int folderId, int destinationFolderId) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/MoveFolder?folderId={folderId}&destinationFolderId={destinationFolderId}";
        public string DeleteFolderAsync(int folderId) =>
                    $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/DeleteFolder?folderId={folderId}";
        public string DeleteFileAsync(int mediaId) =>
                    $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/DeleteFile?mediaId={mediaId}";

    }
}
