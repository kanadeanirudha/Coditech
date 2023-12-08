using Coditech.API.Data;
using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
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

        [HttpGet]
        [Route("/User/GetActiveModuleList")]
        [Produces(typeof(UserModuleListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetActiveModuleList()
        {
            try
            {
                List<UserModuleMaster> list = _userService.GetActiveModuleList();
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<UserModuleListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModule.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new UserModuleListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModule.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new UserModuleListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/User/GetActiveMenuList")]
        [HttpGet]
        [Produces(typeof(UserMenuListResponse))]
        public virtual IActionResult GetActiveMenuList(string moduleCode)
        {
            try
            {
                List<UserMainMenuMaster> list = _userService.GetActiveMenuList(moduleCode);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<UserMenuListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenu.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new UserMenuListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenu.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new UserMenuListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GymMemberDetails/InsertPersonInformation")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralPersonResponse))]
        public virtual IActionResult InsertPersonInformation([FromBody] GeneralPersonModel model)
        {
            try
            {
                GeneralPersonModel generalPerson = _userService.InsertPersonInformation(model);
                return HelperUtility.IsNotNull(generalPerson) ? CreateCreatedResponse(new GeneralPersonResponse { GeneralPersonModel = generalPerson }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Person.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPersonResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Person.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPersonResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}