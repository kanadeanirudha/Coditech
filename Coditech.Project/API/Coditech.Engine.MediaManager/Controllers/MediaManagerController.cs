using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;
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
        [HttpPost, ValidateModel]
        [Produces(typeof(MediaManagerResponse))]
        public virtual IActionResult UploadMedia([FromBody] UploadMediaModel model)
        {
            try
            {
                UploadMediaModel uploadMediaModel = _mediaManagerService.UploadMedia(model);
                return IsNotNull(uploadMediaModel) ? CreateCreatedResponse(new MediaManagerResponse { UploadMediaModel = uploadMediaModel }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Warning);
                return CreateUnauthorizedResponse(new MediaManagerResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
                return CreateUnauthorizedResponse(new MediaManagerResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}