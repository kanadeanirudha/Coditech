using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Logger;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace Coditech.API.Controllers
{
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        protected readonly ICoditechLogging _coditechLogging;
        public UserController(ICoditechLogging coditechLogging, IUserService UserService)
        {
            _userService = UserService;
            _coditechLogging = coditechLogging;
        }
        /// <summary>
        /// Login to application.
        /// </summary>
        /// <param name="model">User Model.</param>
        /// <returns>UserModel</returns>
        [Route("/User/Login")]
        [HttpPost, ValidateModel]
        [Produces(typeof(UserModel))]
        public virtual IActionResult Login([FromBody] UserLoginModel model)
        {
            try
            {
                UserModel user = _userService.Login(model);
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

        [Route("/User/GetActiveModuleList")]
        [HttpGet]
        [Produces(typeof(UserModuleResponse))]
        public virtual IActionResult GetActiveModuleList(short userId)
        {
            try
            {
                UserModuleModel userModuleModel = _userService.GetActiveModuleList(userId);
                return HelperUtility.IsNotNull(userModuleModel) ? CreateOKResponse(new UserModuleResponse { UserModuleModel = userModuleModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModule.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new UserModuleResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModule.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new UserModuleResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/User/GetActiveMenuListList")]
        [HttpGet]
        [Produces(typeof(UserMainMenuResponse))]
        public virtual IActionResult GetActiveMenuListList(short moduleCode)
        {
            try
            {
                UserMainMenuModel userMainMenuModel = _userService.GetActiveMenuListList(moduleCode);
                return HelperUtility.IsNotNull(userMainMenuModel) ? CreateOKResponse(new UserMainMenuResponse { UserMainMenuModel = userMainMenuModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenu.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new UserMainMenuResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenu.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new UserMainMenuResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}