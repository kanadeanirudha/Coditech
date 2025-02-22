using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
namespace Coditech.API.Controllers
{
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
        public virtual IActionResult UploadMedia(int folderId, string folderName, long mediaId)
        {
            try
            {
                IEnumerable<IFormFile> files = Request.Form.Files;
                MediaManagerResponse fileUploadListModelResponse = _mediaManagerService.UploadMedia(folderId, folderName, mediaId, files, Request);
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
        [HttpGet]
        [Route("/MediaManager/GetMediaList")]
        [Produces(typeof(MediaManagerFolderResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetMediaList(int rootFolderId, int adminRoleId, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                MediaManagerFolderResponse list = _mediaManagerService.GetMediaList(rootFolderId, adminRoleId, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<MediaManagerFolderResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new MediaManagerFolderResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new MediaManagerFolderResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/MediaManager/GetMediaDetails")]
        [HttpGet]
        [Produces(typeof(MediaManagerResponse))]
        public IActionResult GetMediaDetails(long mediaId)
        {
            try
            {
                MediaModel bioradMedisyMediaModel = _mediaManagerService.GetMediaDetails(mediaId);
                return HelperUtility.IsNotNull(bioradMedisyMediaModel) ? CreateOKResponse(new MediaManagerResponse { MediaModel = bioradMedisyMediaModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new MediaManagerResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new MediaManagerResponse { HasError = true, ErrorMessage = ex.Message });
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



        [Route("/MediaManager/GetMoveFolders")]
        [HttpGet]
        [Produces(typeof(FolderListResponse))]
        public virtual IActionResult GetMoveFolders(int moveFolderId)
        {
            try
            {
                FolderListResponse response = _mediaManagerService.GetMoveFolders(moveFolderId);
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