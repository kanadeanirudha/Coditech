using Coditech.API.Service;
using Coditech.Common.API;
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
        /// Login to application.
        /// </summary>
        /// <param name="model">UploadMediaModel.</param>
        /// <returns>UploadMediaModel</returns>
        [Route("/MediaManager/UploadMedia")]
        [AllowAnonymous]
        [HttpPost]
        [Produces(typeof(FileUploadListModelResponse))]
        public virtual async Task<IActionResult> PostAsync()
        {
            try
            {
                IEnumerable<IFormFile> files = Request.Form.Files;
                FileUploadListModelResponse fileUploadListModelResponse = _mediaManagerService.UploadServerFiles(files, Request).Result;
                if (fileUploadListModelResponse != null)
                    return CreateOKResponse<FileUploadListModelResponse>(fileUploadListModelResponse);
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