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
        [Produces(typeof(AdminRoleApplicableDetailsListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetAdminRoleList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                AdminRoleListModel list = _adminRoleMasterService.GetAdminRoleList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AdminRoleApplicableDetailsListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleApplicableDetailsListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleApplicableDetailsListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminRoleMaster/GetAdminRoleDetailsById")]
        [HttpGet]
        [Produces(typeof(AdminRoleResponse))]
        public virtual IActionResult GetAdminRoleDetailsById(int adminRoleMasterId)
        {
            try
            {
                AdminRoleModel adminRoleModel = _adminRoleMasterService.GetAdminRoleDetailsById(adminRoleMasterId);
                return IsNotNull(adminRoleModel) ? CreateOKResponse(new AdminRoleResponse() { AdminRoleModel = adminRoleModel }) : NotFound();
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

        [Route("/AdminRoleMaster/GetAdminRoleMenuDetailsById")]
        [HttpGet]
        [Produces(typeof(AdminRoleMenuDetailsResponse))]
        public virtual IActionResult GetAdminRoleMenuDetailsById(int adminRoleMasterId, string moduleCode)
        {
            try
            {
                AdminRoleMenuDetailsModel adminRoleMenuDetailsModel = _adminRoleMasterService.GetAdminRoleMenuDetailsById(adminRoleMasterId, moduleCode);
                return IsNotNull(adminRoleMenuDetailsModel) ? CreateOKResponse(new AdminRoleMenuDetailsResponse() { AdminRoleMenuDetailsModel = adminRoleMenuDetailsModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminRoleMenuDetailsResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleMenuDetailsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminRoleMaster/InsertUpdateAdminRoleMenuDetails")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AdminRoleMenuDetailsResponse))]
        public virtual IActionResult InsertUpdateAdminRoleMenuDetails([FromBody] AdminRoleMenuDetailsModel model)
        {
            try
            {
                bool isUpdated = _adminRoleMasterService.InsertUpdateAdminRoleMenuDetails(model);
                return isUpdated ? CreateOKResponse(new AdminRoleMenuDetailsResponse { AdminRoleMenuDetailsModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminRoleMenuDetailsResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleMenuDetailsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("/AdminRoleMaster/RoleAllocatedToUserList")]
        [Produces(typeof(AdminRoleApplicableDetailsListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult RoleAllocatedToUserList(int adminRoleMasterId, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                AdminRoleApplicableDetailsListModel list = _adminRoleMasterService.RoleAllocatedToUserList(adminRoleMasterId, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                return IsNotNull(list) ? CreateOKResponse(new AdminRoleApplicableDetailsListResponse() { AdminRoleApplicableDetailsList = list, PageIndex = list.PageIndex, PageSize = list.PageSize, TotalPages = list.TotalPages, TotalResults = list.TotalResults }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleApplicableDetailsListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleApplicableDetailsListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminRoleMaster/GetAssociateUnAssociateAdminRoleToUser")]
        [HttpGet]
        [Produces(typeof(AdminRoleApplicableDetailsResponse))]
        public virtual IActionResult GetAssociateUnAssociateAdminRoleToUser(int adminRoleMasterId, int adminRoleApplicableDetailId)
        {
            try
            {
                AdminRoleApplicableDetailsModel adminRoleApplicableDetailsModel = _adminRoleMasterService.GetAssociateUnAssociateAdminRoleToUser(adminRoleMasterId, adminRoleApplicableDetailId);
                return IsNotNull(adminRoleApplicableDetailsModel) ? CreateOKResponse(new AdminRoleApplicableDetailsResponse() { AdminRoleApplicableDetailsModel = adminRoleApplicableDetailsModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminRoleApplicableDetailsResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleApplicableDetailsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminRoleMaster/AssociateUnAssociateAdminRoleToUser")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AdminRoleApplicableDetailsResponse))]
        public virtual IActionResult AssociateUnAssociateAdminRoleToUser([FromBody] AdminRoleApplicableDetailsModel model)
        {
            try
            {
                bool isUpdated = _adminRoleMasterService.AssociateUnAssociateAdminRoleToUser(model);
                return isUpdated ? CreateOKResponse(new AdminRoleApplicableDetailsResponse { AdminRoleApplicableDetailsModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminRoleApplicableDetailsResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleApplicableDetailsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminRoleMaster/GetAdminRoleWiseMediaFolderActionById")]
        [HttpGet]
        [Produces(typeof(AdminRoleMediaFolderActionResponse))]
        public virtual IActionResult GetAdminRoleWiseMediaFolderActionById(int adminRoleMasterId)
        {
            try
            {
                AdminRoleMediaFolderActionModel adminRoleMediaFolderActionModel = _adminRoleMasterService.GetAdminRoleWiseMediaFolderActionById(adminRoleMasterId);
                return IsNotNull(adminRoleMediaFolderActionModel) ? CreateOKResponse(new AdminRoleMediaFolderActionResponse() { AdminRoleMediaFolderActionModel = adminRoleMediaFolderActionModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminRoleMediaFolderActionResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleMediaFolderActionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminRoleMaster/InsertUpdateAdminRoleWiseMediaFolderAction")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AdminRoleMediaFolderActionResponse))]
        public virtual IActionResult InsertUpdateAdminRoleWiseMediaFolderAction([FromBody] AdminRoleMediaFolderActionModel model)
        {
            try
            {
                bool isUpdated = _adminRoleMasterService.InsertUpdateAdminRoleWiseMediaFolderAction(model);
                return isUpdated ? CreateOKResponse(new AdminRoleMediaFolderActionResponse { AdminRoleMediaFolderActionModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminRoleMediaFolderActionResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleMediaFolderActionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminRoleMaster/GetAdminRoleWiseMediaFoldersById")]
        [HttpGet]
        [Produces(typeof(AdminRoleMediaFoldersResponse))]
        public virtual IActionResult GetAdminRoleWiseMediaFoldersById(int adminRoleMasterId)
        {
            try
            {
                AdminRoleMediaFoldersModel adminRoleMediaFoldersModel = _adminRoleMasterService.GetAdminRoleWiseMediaFoldersById(adminRoleMasterId);
                return IsNotNull(adminRoleMediaFoldersModel) ? CreateOKResponse(new AdminRoleMediaFoldersResponse() { AdminRoleMediaFoldersModel = adminRoleMediaFoldersModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminRoleMediaFoldersResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleMediaFoldersResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminRoleMaster/InsertUpdateAdminRoleWiseMediaFolders")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AdminRoleMediaFoldersResponse))]
        public virtual IActionResult InsertUpdateAdminRoleWiseMediaFolders([FromBody] AdminRoleMediaFoldersModel model)
        {
            try
            {
                bool isUpdated = _adminRoleMasterService.InsertUpdateAdminRoleWiseMediaFolders(model);
                return isUpdated ? CreateOKResponse(new AdminRoleMediaFoldersResponse { AdminRoleMediaFoldersModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminRoleMediaFoldersResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminRoleMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminRoleMediaFolderActionResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}