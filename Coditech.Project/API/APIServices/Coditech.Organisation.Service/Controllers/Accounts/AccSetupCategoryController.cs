using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Coditech.Common.Logger;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Coditech.API.Controllers
{
    public class AccSetupCategoryController : BaseController
    {
        private readonly IAccSetupCategoryService _accSetupCategoryService;
        protected readonly ICoditechLogging _coditechLogging;
        public AccSetupCategoryController(ICoditechLogging coditechLogging, IAccSetupCategoryService accSetupCategoryService)
        {
            _accSetupCategoryService = accSetupCategoryService;
            _coditechLogging = coditechLogging;
        }

        [Route("/AccSetupCategory/GetAccSetupCategory")]
        [HttpGet]
        [Produces(typeof(AccSetupCategoryListResponse))]
        public virtual IActionResult GetAccSetupCategory()
        {
            try
            {
                AccSetupCategoryListModel list = _accSetupCategoryService.GetAccSetupCategory();
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AccSetupCategoryListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupCategory.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupCategoryListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupCategory.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupCategoryListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
