﻿using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;

namespace Coditech.API.Service
{
    public interface IMediaManagerService
    {
        UploadMediaModel UploadMedia(UploadMediaModel model);
        Task<MediaManagerResponse> UploadServerFiles(IEnumerable<IFormFile> files, HttpRequest request);
        Task<MediaManagerFolderResponse> GetFolderStructure(int rootFolderId = 0);
        Task<bool> PostRenameFolder(int FolderId, string RenameFolderName);
        Task<TrueFalseResponse> PostCreateFolder(int RootFolderId, string FolderName);
        Task<bool> UploadFile(IFormFile formFile, int folderId, HttpRequest request);
        Task<FolderListResponse> GetAllFolders();
        Task<bool> MoveFolder(int folderId, int destinationFolderId);
        Task<bool> DeleteFolder(int folderId);
    }
}
