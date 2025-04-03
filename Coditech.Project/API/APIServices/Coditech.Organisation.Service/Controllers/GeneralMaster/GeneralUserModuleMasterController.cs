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
    public class GeneralUserModuleMasterController : BaseController
    {
        private readonly IGeneralUserModuleMasterService _generalUserModuleMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralUserModuleMasterController(ICoditechLogging coditechLogging, IGeneralUserModuleMasterService generalUserModuleMasterService)
        {
            _generalUserModuleMasterService = generalUserModuleMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralUserModuleMaster/GetUserModuleList")]
        [Produces(typeof(UserModuleListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetUserModuleList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                UserModuleListModel list = _generalUserModuleMasterService.GetUserModuleList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<UserModuleListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new UserModuleListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new UserModuleListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralUserModuleMaster/CreateUserModule")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralUserModuleResponse))]
        public virtual IActionResult CreateUserMainMenu([FromBody] UserModuleModel model)
        {
            try
            {
                UserModuleModel userModuleMaster = _generalUserModuleMasterService.CreateUserModule(model);
                return IsNotNull(userModuleMaster) ? CreateCreatedResponse(new GeneralUserModuleResponse { UserModuleModel = userModuleMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralUserModuleResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralUserModuleResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralUserModuleMaster/GetUserModule")]
        [HttpGet]
        [Produces(typeof(GeneralUserModuleResponse))]
        public virtual IActionResult GetUserModule(short userModuleMasterId)
        {
            try
            {
                UserModuleModel generalUserModuleModel = _generalUserModuleMasterService.GetUserModule(userModuleMasterId);
                return IsNotNull(generalUserModuleModel) ? CreateOKResponse(new GeneralUserModuleResponse { UserModuleModel = generalUserModuleModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralUserModuleResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralUserModuleResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralUserModuleMaster/UpdateUserModule")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralUserModuleResponse))]
        public virtual IActionResult UpdateUserModule([FromBody] UserModuleModel model)
        {
            try
            {
                bool isUpdated = _generalUserModuleMasterService.UpdateUserModule(model);
                return isUpdated ? CreateOKResponse(new GeneralUserModuleResponse { UserModuleModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralUserModuleResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralUserModuleResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralUserModuleMaster/DeleteUserModule")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteUserModule([FromBody] ParameterModel userModuleMasterIds)
        {
            try
            {
                bool deleted = _generalUserModuleMasterService.DeleteUserModule(userModuleMasterIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}