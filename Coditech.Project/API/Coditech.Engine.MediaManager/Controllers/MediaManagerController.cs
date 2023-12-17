using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Logger;

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
            _mediaManagerService = mediaManagerService;
            _coditechLogging = coditechLogging;
        }
   
        /// <summary>
        /// Login to application.
        /// </summary>
        /// <param name="model">User Model.</param>
        /// <returns>UserModel</returns>
        [Route("/MediaManager/UploadMedia")]
        [HttpPost, ValidateModel]
        [Produces(typeof(UserModel))]
        public virtual IActionResult UploadMedia([FromBody] UploadMediaModel model)
        {
            try
            {
                UploadMediaModel uploadMediaModel = _mediaManagerService.UploadMedia(model);
                return HelperUtility.IsNotNull(uploadMediaModel) ? CreateOKResponse(uploadMediaModel) : null;

            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Warning);
                return CreateUnauthorizedResponse(new UserModel { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MediaManager.ToString(), TraceLevel.Error);
                return CreateUnauthorizedResponse(new UserModel { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}