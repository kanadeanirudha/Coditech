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
    public class GymUserController : BaseController
    {

        private readonly IGymUserService _gymUserService;
        protected readonly ICoditechLogging _coditechLogging;
        public GymUserController(ICoditechLogging coditechLogging, IGymUserService gymUserService)
        {
            _gymUserService = gymUserService;
            _coditechLogging = coditechLogging;
        }
        /// <summary>
        /// Login to application.
        /// </summary>
        /// <param name="model">UserLoginModel.</param>
        /// <returns>UserModel</returns>
        [Route("/GymUser/Login")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GymUserModel))]
        public virtual IActionResult Login([FromBody] UserLoginModel model)
        {
            try
            {
                GymUserModel user = _gymUserService.Login(model);
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

        [Route("/GymUser/ChangePassword")]
        [HttpPost, ValidateModel]
        [Produces(typeof(ChangePasswordResponse))]
        public virtual IActionResult ChangePassword([FromBody] ChangePasswordModel model)
        {
            try
            {
                ChangePasswordModel changePassword = _gymUserService.ChangePassword(model);
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