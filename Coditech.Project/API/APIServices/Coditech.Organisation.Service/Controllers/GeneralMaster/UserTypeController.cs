using Coditech.API.Data;
using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Controllers
{
    public class UserTypeController : BaseController
    {
        private readonly IUserTypeService _userTypeService;
        protected readonly ICoditechLogging _coditechLogging;
        public UserTypeController(ICoditechLogging coditechLogging, IUserTypeService userTypeService)
        {
            _userTypeService = userTypeService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/UserType/GetUserTypeList")]
        [Produces(typeof(UserTypeListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetUserTypeList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                UserTypeListModel list = _userTypeService.GetUserTypeList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<UserTypeListResponse>(data) : CreateNoContentResponse();
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

        [Route("/UserType/CreateUserType")]
        [HttpPost, ValidateModel]
        [Produces(typeof(UserTypeResponse))]
        public virtual IActionResult CreateUserType([FromBody] UserTypeModel model)
        {
            try
            {
                UserTypeModel usertype = _userTypeService.CreateUserType(model);
                return IsNotNull(usertype) ? CreateCreatedResponse(new UserTypeResponse { UserTypeModel = usertype }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new UserTypeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new UserTypeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/UserType/GetUserType")]
        [HttpGet]
        [Produces(typeof(UserTypeResponse))]
        public virtual IActionResult GetUserType(short userTypeId)
        {
            try
            {
                UserTypeModel userType = _userTypeService.GetUserType(userTypeId);
                return IsNotNull(userType) ? CreateOKResponse(new UserTypeResponse { UserTypeModel = userType }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new UserTypeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new UserTypeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/UserType/UpdateUserType")]
        [HttpPut, ValidateModel]
        [Produces(typeof(UserTypeResponse))]
        public virtual IActionResult UpdateUserType([FromBody] UserTypeModel model)
        {
            try
            {
                bool isUpdated = _userTypeService.UpdateUserType(model);
                return isUpdated ? CreateOKResponse(new UserTypeResponse { UserTypeModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new UserTypeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new UserTypeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/UserType/DeleteUserType")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteUserType([FromBody] ParameterModel userTypeIds)
        {
            try
            {
                bool deleted = _userTypeService.DeleteUserType(userTypeIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}