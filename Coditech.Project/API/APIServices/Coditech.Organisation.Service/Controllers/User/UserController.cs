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

using static Coditech.Common.Helper.HelperUtility;
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

        #region
        [Route("/User/ResetPassword")]
        [HttpPost, ValidateModel]
        [Produces(typeof(ResetPasswordResponse))]
        public virtual IActionResult ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            try
            {
                ResetPasswordModel resetPassword = _userService.ResetPassword(resetPasswordModel);
                return IsNotNull(resetPassword) ? CreateCreatedResponse(new ResetPasswordResponse { ResetPasswordModel = resetPassword }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechUnauthorizedException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.ResetPassword.ToString(), TraceLevel.Warning);
                return CreateUnauthorizedResponse(new ResetPasswordResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.ResetPassword.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new ResetPasswordResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.ResetPassword.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new ResetPasswordResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("/User/ResetPasswordSendLink")]
        [Produces(typeof(ResetPasswordSendLinkResponse))]
        public virtual IActionResult ResetPasswordSendLink(string userName, bool isMobileRequest = false)
        {
            try
            {
                ResetPasswordSendLinkModel resetPasswordSendLink = _userService.ResetPasswordSendLink(userName, isMobileRequest);
                return IsNotNull(resetPasswordSendLink) ? CreateCreatedResponse(new ResetPasswordSendLinkResponse { ResetPasswordSendLinkModel = resetPasswordSendLink }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechUnauthorizedException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.ResetPasswordSendLink.ToString(), TraceLevel.Warning);
                return CreateUnauthorizedResponse(new ResetPasswordSendLinkResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.ResetPasswordSendLink.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new ResetPasswordSendLinkResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.ResetPasswordSendLink.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new ResetPasswordSendLinkResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        #endregion

        [Route("/User/ChangePassword")]
        [HttpPost, ValidateModel]
        [Produces(typeof(ChangePasswordResponse))]
        public virtual IActionResult ChangePassword([FromBody] ChangePasswordModel model)
        {
            try
            {
                ChangePasswordModel changePassword = _userService.ChangePassword(model);
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

        [HttpGet]
        [Route("/User/GetActiveModuleList")]
        [Produces(typeof(UserModuleListResponse))]
        public virtual IActionResult GetActiveModuleList()
        {
            try
            {
                List<UserModuleModel> list = _userService.GetActiveModuleList();
                return IsNotNull(list) ? CreateOKResponse(new UserModuleListResponse { ModuleList = list }) : CreateNoContentResponse();
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
        [Produces(typeof(UserMainMenuListResponse))]
        public virtual IActionResult GetActiveMenuList(string moduleCode)
        {
            try
            {
                List<UserMainMenuModel> list = _userService.GetActiveMenuList(moduleCode);
                return IsNotNull(list) ? CreateOKResponse(new UserMainMenuListResponse { MenuList = list }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenu.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new UserMainMenuListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenu.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new UserMainMenuListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/User/InsertPersonInformation")]
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

        [Route("/User/GetPersonInformation")]
        [HttpGet]
        [Produces(typeof(GeneralPersonResponse))]
        public virtual IActionResult GetPersonInformation(long personId)
        {
            try
            {
                GeneralPersonModel generalPersonModel = _userService.GetPersonInformation(personId);
                return HelperUtility.IsNotNull(generalPersonModel) ? CreateOKResponse(new GeneralPersonResponse() { GeneralPersonModel = generalPersonModel }) : NotFound();
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

        [Route("/User/UpdatePersonInformation")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralPersonResponse))]
        public virtual IActionResult UpdatePersonInformation([FromBody] GeneralPersonModel model)
        {
            try
            {
                bool isUpdated = _userService.UpdatePersonInformation(model);
                return isUpdated ? CreateOKResponse(new GeneralPersonResponse { GeneralPersonModel = model }) : CreateInternalServerErrorResponse();
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


        //get Person Address Details
        [Route("/User/GetGeneralPersonAddresses")]
        [HttpGet]
        [Produces(typeof(GeneralPersonAddressListResponse))]
        public virtual IActionResult GetGeneralPersonAddresses(long personId)
        {
            try
            {
                GeneralPersonAddressListModel generalPersonAddressList = _userService.GetGeneralPersonAddresses(personId);
                return HelperUtility.IsNotNull(generalPersonAddressList) ? CreateOKResponse(new GeneralPersonAddressListResponse() { GeneralPersonAddressList = generalPersonAddressList.PersonAddressList, FirstName = generalPersonAddressList.FirstName, LastName = generalPersonAddressList.LastName }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Person.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPersonAddressListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Person.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPersonAddressListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/User/InsertUpdateGeneralPersonAddress")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralPersonAddressResponse))]
        public virtual IActionResult InsertUpdateGeneralPersonAddress([FromBody] GeneralPersonAddressModel model)
        {
            try
            {
                GeneralPersonAddressModel generalPersonAddress = _userService.InsertUpdateGeneralPersonAddress(model);
                return HelperUtility.IsNotNull(generalPersonAddress) ? CreateCreatedResponse(new GeneralPersonAddressResponse { GeneralPersonAddressModel = generalPersonAddress }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Person.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPersonAddressResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Person.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPersonAddressResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/User/AcceptTermsAndConditions")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult AcceptTermsAndConditions(string userType, long entityId)
        {
            try
            {
                bool isAccepted = _userService.AcceptTermsAndConditions(userType, entityId);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = isAccepted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Person.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Person.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [HttpGet]
        [Route("/User/GetUserTypeList")]
        [Produces(typeof(UserTypeListResponse))]
        public virtual IActionResult GetUserTypeList()
        {
            try
            {
                List<UserTypeModel> list = _userService.GetUserTypeList();
                return IsNotNull(list) ? CreateOKResponse(new UserTypeListResponse { TypeList = list }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new UserTypeListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new UserTypeListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}