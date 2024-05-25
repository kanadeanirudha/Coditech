using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Controllers
{
    [ApiController]
    public class UserMobileAppController : BaseController
    {

        private readonly IUserMobileAppService _userMobileAppService;
        protected readonly ICoditechLogging _coditechLogging;
        public UserMobileAppController(ICoditechLogging coditechLogging, IUserMobileAppService UserMobileAppService)
        {
            _userMobileAppService = UserMobileAppService;
            _coditechLogging = coditechLogging;
        }
        /// <summary>
        /// Login to application.
        /// </summary>
        /// <param name="model">UserLoginModel.</param>
        /// <returns>UserModel</returns>
        [Route("/UserMobileApp/Login")]
        [HttpPost, ValidateModel]
        [Produces(typeof(UserMobileAppModel))]
        public virtual IActionResult Login([FromBody] UserLoginModel model)
        {
            try
            {
                UserMobileAppModel user = _userMobileAppService.Login(model);
                return HelperUtility.IsNotNull(user) ? CreateOKResponse(user) : null;

            }
            catch (CoditechUnauthorizedException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserLogin.ToString(), TraceLevel.Warning);
                return CreateUnauthorizedResponse(new UserModel { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserLogin.ToString(), TraceLevel.Warning);
                return CreateUnauthorizedResponse(new UserModel { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserLogin.ToString(), TraceLevel.Error);
                return CreateUnauthorizedResponse(new UserModel { HasError = true, ErrorMessage = ex.Message });
            }

        }

        [Route("/UserMobileApp/ChangePassword")]
        [HttpPost, ValidateModel]
        [Produces(typeof(ChangePasswordResponse))]
        public virtual IActionResult ChangePassword([FromBody] ChangePasswordModel model)
        {
            try
            {
                ChangePasswordModel changePassword = _userMobileAppService.ChangePassword(model);
                return IsNotNull(changePassword) ? CreateCreatedResponse(new ChangePasswordResponse { ChangePasswordModel = changePassword }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.ChangePassword.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new ChangePasswordResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.ChangePassword.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new ChangePasswordResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}