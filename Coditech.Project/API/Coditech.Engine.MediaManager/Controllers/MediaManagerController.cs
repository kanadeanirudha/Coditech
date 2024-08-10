using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
namespace Coditech.API.Controllers
{
    [ApiController]
    public class MediaManagerController : BaseController
    {
        private readonly IMediaManagerService _mediaManagerService;
        protected readonly ICoditechLogging _coditechLogging;

        public MediaManagerController(ICoditechLogging coditechLogging, IMediaManagerService mediaManagerService)
        {
            _coditechLogging = coditechLogging;
            _mediaManagerService = mediaManagerService;
        }

        /// <summary>
        /// Upload Media
        /// </summary>
        /// <param name="model">UploadMediaModel.</param>
        /// <returns>UploadMediaModel</returns>
        [Route("/MediaManager/UploadMedia")]
        [AllowAnonymous]
        [HttpPost]
        [Produces(typeof(MediaManagerResponse))]
        public virtual async Task<IActionResult> PostAsync()
        {
            try
            {
                IEnumerable<IFormFile> files = Request.Form.Files;
                MediaManagerResponse fileUploadListModelResponse = await _mediaManagerService.UploadServerFiles(files, Request);
                if (fileUploadListModelResponse != null)
                    return CreateOKResponse<MediaManagerResponse>(fileUploadListModelResponse);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse();
            }

        }

        [Route("/MediaManager/UploadFile")]
        [HttpPost]
        [Produces(typeof(bool))]
        public virtual async Task<IActionResult> PostUploadFileAsync(int folderId)
        {
            try
            {
                IEnumerable<IFormFile> files = Request.Form.Files;
                bool response = await _mediaManagerService.UploadFile(files.FirstOrDefault(), folderId, Request);
                if (response)
                    return CreateOKResponse<bool>(response);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse();
            }
        }

        /// <summary>
        /// Login to application.
        /// </summary>
        /// <param name="model">UploadMediaModel.</param>
        /// <returns>UploadMediaModel</returns>
        [Route("/MediaManager/FolderStructure")]
        [HttpGet]
        [Produces(typeof(MediaManagerFolderResponse))]
        public virtual async Task<IActionResult> GetFolderStructure(int rootFolderId = 0)
        {
            try
            {
                MediaManagerFolderResponse MediaManagerFolderResponse = await _mediaManagerService.GetFolderStructure(rootFolderId);
                if (MediaManagerFolderResponse != null)
                    return CreateOKResponse<MediaManagerFolderResponse>(MediaManagerFolderResponse);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse();
            }
        }

        [Route("/MediaManager/CreateFolder")]
        [HttpGet]
        [Produces(typeof(TrueFalseResponse))]
        public virtual async Task<IActionResult> CreateFolder(int rootFolderId, string folderName)
        {
            try
            {
                TrueFalseResponse response = await _mediaManagerService.PostCreateFolder(rootFolderId, folderName);
                if (response != null)
                    return CreateOKResponse<TrueFalseResponse>(response);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse();
            }
        }

        [Route("/MediaManager/RenameFolder")]
        [HttpGet]
        [Produces(typeof(bool))]
        public virtual async Task<IActionResult> PostRenameFolder(int folderId, string renameFolderName)
        {
            try
            {
                bool response = await _mediaManagerService.PostRenameFolder(folderId, renameFolderName);
                if (response)
                    return CreateOKResponse<bool>(response);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse();
            }
        }

        

        [Route("/MediaManager/GetAllFolders")]
        [HttpGet]
        [Produces(typeof(FolderListResponse))]
        public virtual async Task<IActionResult> GetAllFolders()
        {
            try
            {
                FolderListResponse response = await _mediaManagerService.GetAllFolders();
                if (response != null)
                    return CreateOKResponse<FolderListResponse>(response);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse();
            }
        }

        [Route("/MediaManager/MoveFolder")]
        [HttpGet]
        [Produces(typeof(bool))]
        public virtual async Task<IActionResult> MoveFolder(int folderId, int destinationFolderId)
        {
            try
            {
                bool response = await _mediaManagerService.MoveFolder(folderId, destinationFolderId);
                if (response)
                    return CreateOKResponse<bool>(response);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse();
            }
        }

        [Route("/MediaManager/DeleteFolder")]
        [HttpGet]
        [Produces(typeof(bool))]
        public virtual async Task<IActionResult> DeleteFolder(int folderId)
        {
            try
            {
                bool response = await _mediaManagerService.DeleteFolder(folderId);
                if (response)
                    return CreateOKResponse<bool>(response);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse();
            }
        }

        [Route("/MediaManager/DeleteFile")]
        [HttpGet]
        [Produces(typeof(bool))]
        public virtual async Task<IActionResult> DeleteFile(int mediaId)
        {
            try
            {
                bool response = await _mediaManagerService.DeleteFile(mediaId);
                if (response)
                    return CreateOKResponse<bool>(response);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse();
            }
        }
    }
}