using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
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
        public virtual IActionResult UploadMedia(int folderId, string folderName)
        {
            try
            {
                IEnumerable<IFormFile> files = Request.Form.Files;
                MediaManagerResponse fileUploadListModelResponse = _mediaManagerService.UploadMedia(folderId, folderName, files, Request);
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


        /// <summary>
        /// Get Folder Structure
        /// </summary>
        /// <param name="model">UploadMediaModel.</param>
        /// <returns>UploadMediaModel</returns>
        [Route("/MediaManager/FolderStructure")]
        [HttpGet]
        [Produces(typeof(MediaManagerFolderResponse))]
        public virtual IActionResult GetFolderStructure([FromQuery] int rootFolderId = 0, [FromQuery] int adminRoleId = 0, [FromQuery] bool isAdminUser = false, [FromQuery] FilterCollection filter = null, [FromQuery] ExpandCollection expand = null, [FromQuery] SortCollection sort = null, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 0)
        {
            try
            {
                MediaManagerFolderResponse MediaManagerFolderResponse = _mediaManagerService.GetFolderStructure(rootFolderId, adminRoleId, isAdminUser, pageIndex, pageSize);
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
        public virtual IActionResult CreateFolder(int rootFolderId, string folderName, int adminRoleMasterId)
        {
            try
            {
                TrueFalseResponse response = _mediaManagerService.PostCreateFolder(rootFolderId, folderName, adminRoleMasterId);
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
        public virtual IActionResult PostRenameFolder(int folderId, string renameFolderName)
        {
            try
            {
                bool response = _mediaManagerService.PostRenameFolder(folderId, renameFolderName);
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
        public virtual IActionResult GetAllFolders()
        {
            try
            {
                FolderListResponse response = _mediaManagerService.GetAllFolders();
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
        public virtual IActionResult MoveFolder(int folderId, int destinationFolderId)
        {
            try
            {
                bool response = _mediaManagerService.MoveFolder(folderId, destinationFolderId);
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
        public virtual IActionResult DeleteFolder(int folderId)
        {
            try
            {
                bool response = _mediaManagerService.DeleteFolder(folderId);
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
        public virtual IActionResult DeleteFile(int mediaId)
        {
            try
            {
                bool response = _mediaManagerService.DeleteFile(mediaId);
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