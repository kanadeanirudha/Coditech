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
        [Produces(typeof(AdminRoleListResponse))]
        public IActionResult CreateAdminRoleMaster([FromBody] AdminRoleMasterModel model)
        {
            try
            {
                AdminRoleMasterModel adminRoleMaster = _adminRoleMasterService.CreateAdminRoleMaster(model);
                return IsNotNull(adminRoleMaster) ? CreateCreatedResponse(new AdminRoleListResponse { AdminRoleMasterModel = adminRoleMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminRoleListResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminRoleMaster/GetAdminRoleMasterDetailsById")]
        [HttpGet]
        [Produces(typeof(AdminRoleMasterModel))]
        public IActionResult GetAdminRoleMasterDetailsById(short adminRoleMasterId)
        {
            try
            {
                AdminRoleMasterModel adminRoleMasterModel = _adminRoleMasterService.GetAdminRoleMasterDetailsById(adminRoleMasterId);
                return IsNotNull(adminRoleMasterModel) ? CreateOKResponse(adminRoleMasterModel) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminRoleMasterModel { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleMasterModel { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminRoleMaster/UpdateAdminRoleMaster")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AdminRoleListResponse))]
        public IActionResult UpdateAdminRoleMaster([FromBody] AdminRoleMasterModel model)
        {
            try
            {
                bool isUpdated = _adminRoleMasterService.UpdateAdminRoleMaster(model);
                return isUpdated ? CreateOKResponse(new AdminRoleListResponse { AdminRoleMasterModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminRoleListResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleListResponse { HasError = true, ErrorMessage = ex.Message });
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