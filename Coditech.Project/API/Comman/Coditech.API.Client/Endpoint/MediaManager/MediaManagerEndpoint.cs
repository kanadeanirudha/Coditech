﻿using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class MediaManagerEndpoint : BaseEndpoint
    {
        public string UploadMediaAsync(int folderId, string folderName, long mediaId) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/UploadMedia?folderId={folderId}&folderName={folderName}&mediaId={mediaId}";

        public string GetFolderStructureAsync(int rootfolderId, int adminRoleId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/GetMediaList?rootFolderId={rootfolderId}&adminRoleId={adminRoleId}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string GetMediaDetailsAsync(long mediaId) =>
            $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/GetMediaDetails?mediaId={mediaId}";

        public string CreateFolderAsync(int rootFolderId, string folderName, int adminRoleMasterId) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/CreateFolder?rootFolderId={rootFolderId}&folderName={folderName}&adminRoleMasterId={adminRoleMasterId}";
        public string RenameFolderAsync(int folderId, string renameFolderName) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/RenameFolder?folderId={folderId}&renameFolderName={renameFolderName}";
        public string GetMoveFolders(int moveFolderId) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/GetMoveFolders?moveFolderId={moveFolderId}";
        public string MoveFolderAsync(int folderId, int destinationFolderId) =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/MoveFolder?folderId={folderId}&destinationFolderId={destinationFolderId}";
        public string DeleteFolderAsync(int folderId) =>
                    $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/DeleteFolder?folderId={folderId}";
        public string DeleteFileAsync(int mediaId) =>
                    $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/DeleteFile?mediaId={mediaId}";

    }
}
