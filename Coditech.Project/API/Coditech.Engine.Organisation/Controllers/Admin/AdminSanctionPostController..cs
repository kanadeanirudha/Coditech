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
    public class AdminSanctionPostController : BaseController
    {
        private readonly IAdminSanctionPostService _adminSanctionPostMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public AdminSanctionPostController(ICoditechLogging coditechLogging, IAdminSanctionPostService adminSanctionPostMasterService)
        {
            _adminSanctionPostMasterService = adminSanctionPostMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/AdminSanctionPost/GetAdminSanctionPostList")]
        [Produces(typeof(AdminSanctionPostListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetAdminSanctionPostList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                AdminSanctionPostListModel list = _adminSanctionPostMasterService.GetAdminSanctionPostList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AdminSanctionPostListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminSanctionPostListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminSanctionPostListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminSanctionPost/CreateAdminSanctionPost")]
        [HttpPost, ValidateModel]
        [Produces(typeof(AdminSanctionPostResponse))]
        public IActionResult CreateAdminSanctionPost([FromBody] AdminSanctionPostModel model)
        {
            try
            {
                AdminSanctionPostModel adminSanctionPost = _adminSanctionPostMasterService.CreateAdminSanctionPost(model);
                return IsNotNull(adminSanctionPost) ? CreateCreatedResponse(new AdminSanctionPostResponse { AdminSanctionPostModel = adminSanctionPost }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminSanctionPostResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminSanctionPostResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminSanctionPost/GetAdminSanctionPost")]
        [HttpGet]
        [Produces(typeof(AdminSanctionPostResponse))]
        public IActionResult GetAdminSanctionPost(int adminSanctionPostId)
        {
            try
            {
                AdminSanctionPostModel adminSanctionPostModel = _adminSanctionPostMasterService.GetAdminSanctionPost(adminSanctionPostId);
                return IsNotNull(adminSanctionPostModel) ? CreateOKResponse(new AdminSanctionPostResponse() { AdminSanctionPostModel = adminSanctionPostModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminSanctionPostResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminSanctionPostResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminSanctionPost/UpdateAdminSanctionPost")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AdminSanctionPostResponse))]
        public IActionResult UpdateAdminSanctionPost([FromBody] AdminSanctionPostModel model)
        {
            try
            {
                bool isUpdated = _adminSanctionPostMasterService.UpdateAdminSanctionPost(model);
                return isUpdated ? CreateOKResponse(new AdminSanctionPostResponse { AdminSanctionPostModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AdminSanctionPostResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AdminSanctionPostResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AdminSanctionPost/DeleteAdminSanctionPost")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteAdminSanctionPost([FromBody] ParameterModel adminSanctionPostIds)
        {
            try
            {
                bool deleted = _adminSanctionPostMasterService.DeleteAdminSanctionPost(adminSanctionPostIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AdminSanctionPost.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}