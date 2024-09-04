using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class MediaManagerEndpoint : BaseEndpoint
    {
        public string UploadMediaAsync() =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/UploadMedia";
        public string GetFolderStructureAsync(int rootfolderId, int adminRoleId, bool isAdminUser) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/FolderStructure?rootFolderId={rootfolderId}&adminRoleId={adminRoleId}&isAdminUser={isAdminUser}";
        public string CreateFolderAsync(int rootFolderId, string folderName) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/CreateFolder?rootFolderId={rootFolderId}&folderName={folderName}";
        public string RenameFolderAsync(int folderId, string renameFolderName) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/RenameFolder?folderId={folderId}&renameFolderName={renameFolderName}";
        public string UploadFileAsync(int folderId) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/UploadFile?folderId={folderId}";
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
