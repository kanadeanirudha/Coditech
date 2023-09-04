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
    public class AdminSnPostsController : BaseController
    {
        private readonly IAdminSnPostsMasterService _adminSnPostsMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public AdminSnPostsController(ICoditechLogging coditechLogging, IAdminSnPostsMasterService adminSnPostsMasterService)
        {
            _adminSnPostsMasterService = adminSnPostsMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/AdminSnPosts/GetAdminSnPostsList")]
        [Produces(typeof(AdminSnPostsListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetAdminSnPostsList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                AdminSnPostsListModel list = _adminSnPostsMasterService.GetAdminSnPostsList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AdminSnPostsListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSnPosts.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminSnPostsListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSnPosts.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminSnPostsListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminSnPosts/CreateAdminSnPosts")]
        [HttpPost, ValidateModel]
        [Produces(typeof(AdminSnPostsListResponse))]
        public IActionResult CreateAdminSnPosts([FromBody] AdminSnPostsModel model)
        {
            try
            {
                AdminSnPostsModel adminSnPosts = _adminSnPostsMasterService.CreateAdminSnPosts(model);
                return IsNotNull(adminSnPosts) ? CreateCreatedResponse(new AdminSnPostsListResponse { AdminSnPostsModel = adminSnPosts }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSnPosts.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminSnPostsListResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSnPosts.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminSnPostsListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminSnPosts/GetAdminSnPosts")]
        [HttpGet]
        [Produces(typeof(AdminSnPostsModel))]
        public IActionResult GetAdminSnPosts(short adminSactionPostId)
        {
            try
            {
                AdminSnPostsModel adminSnPostsModel = _adminSnPostsMasterService.GetAdminSnPosts(adminSactionPostId);
                return IsNotNull(adminSnPostsModel) ? CreateOKResponse(adminSnPostsModel) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSnPosts.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminSnPostsModel { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSnPosts.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminSnPostsModel { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminSnPosts/UpdateAdminSnPosts")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AdminSnPostsListResponse))]
        public IActionResult UpdateAdminSnPosts([FromBody] AdminSnPostsModel model)
        {
            try
            {
                bool isUpdated = _adminSnPostsMasterService.UpdateAdminSnPosts(model);
                return isUpdated ? CreateOKResponse(new AdminSnPostsListResponse { AdminSnPostsModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSnPosts.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminSnPostsListResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSnPosts.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminSnPostsListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminSnPosts/DeleteAdminSnPosts")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteAdminSnPosts([FromBody] ParameterModel adminSnPostsIds)
        {
            try
            {
                bool deleted = _adminSnPostsMasterService.DeleteAdminSnPosts(adminSnPostsIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSnPosts.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSnPosts.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}