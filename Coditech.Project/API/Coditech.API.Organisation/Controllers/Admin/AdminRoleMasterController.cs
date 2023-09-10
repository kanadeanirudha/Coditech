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
    public class AdminRoleMasterController : BaseController
    {
        private readonly IAdminRoleMasterService _adminRoleMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public AdminRoleMasterController(ICoditechLogging coditechLogging, IAdminRoleMasterService adminRoleMasterService)
        {
            _adminRoleMasterService = adminRoleMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/AdminRoleMaster/GetAdminRoleMasterList")]
        [Produces(typeof(AdminRoleListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetAdminRoleMasterList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                AdminRoleMasterListModel list = _adminRoleMasterService.GetAdminRoleMasterList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AdminRoleListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminRoleMaster/CreateAdminRoleMaster")]
        [HttpPost, ValidateModel]
        [Produces(typeof(AdminRoleResponse))]
        public virtual IActionResult CreateAdminRoleMaster([FromBody] AdminRoleModel model)
        {
            try
            {
                AdminRoleModel adminRoleMaster = _adminRoleMasterService.CreateAdminRoleMaster(model);
                return IsNotNull(adminRoleMaster) ? CreateCreatedResponse(new AdminRoleResponse { AdminRoleModel = adminRoleMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminRoleResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminRoleMaster/GetAdminRoleMasterDetailsById")]
        [HttpGet]
        [Produces(typeof(AdminRoleModel))]
        public virtual IActionResult GetAdminRoleMasterDetailsById(short adminRoleMasterId)
        {
            try
            {
                AdminRoleModel adminRoleMasterModel = _adminRoleMasterService.GetAdminRoleMasterDetailsById(adminRoleMasterId);
                return IsNotNull(adminRoleMasterModel) ? CreateOKResponse(adminRoleMasterModel) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminRoleModel { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleModel { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminRoleMaster/UpdateAdminRoleMaster")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AdminRoleResponse))]
        public virtual IActionResult UpdateAdminRoleMaster([FromBody] AdminRoleModel model)
        {
            try
            {
                bool isUpdated = _adminRoleMasterService.UpdateAdminRoleMaster(model);
                return isUpdated ? CreateOKResponse(new AdminRoleResponse { AdminRoleModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminRoleResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminRoleMaster/DeleteAdminRoleMaster")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteAdminRoleMaster([FromBody] ParameterModel adminRoleMasterIds)
        {
            try
            {
                bool deleted = _adminRoleMasterService.DeleteAdminRoleMaster(adminRoleMasterIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}