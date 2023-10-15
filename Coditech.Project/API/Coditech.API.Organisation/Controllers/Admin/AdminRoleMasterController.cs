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
        [Route("/AdminRoleMaster/GetAdminRoleList")]
        [Produces(typeof(AdminRoleListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetAdminRoleList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                AdminRoleListModel list = _adminRoleMasterService.GetAdminRoleList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
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

        [Route("/AdminRoleMaster/GetAdminRoleDetailsById")]
        [HttpGet]
        [Produces(typeof(AdminRoleResponse))]
        public virtual IActionResult GetAdminRoleDetailsById(short adminRoleMasterId)
        {
            try
            {
                AdminRoleModel adminRoleModel = _adminRoleMasterService.GetAdminRoleDetailsById(adminRoleMasterId);
                return IsNotNull(adminRoleModel) ? CreateOKResponse(new AdminRoleResponse(){ AdminRoleModel = adminRoleModel }) : NotFound();
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

        [Route("/AdminRoleMaster/UpdateAdminRole")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AdminRoleResponse))]
        public virtual IActionResult UpdateAdminRole([FromBody] AdminRoleModel model)
        {
            try
            {
                bool isUpdated = _adminRoleMasterService.UpdateAdminRole(model);
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