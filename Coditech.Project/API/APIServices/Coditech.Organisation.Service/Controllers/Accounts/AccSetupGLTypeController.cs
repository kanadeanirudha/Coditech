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
    public class AccSetupGLTypeController : BaseController
    {
        private readonly IAccSetupGLTypeService _accSetupGLTypeService;
        protected readonly ICoditechLogging _coditechLogging;
        public AccSetupGLTypeController(ICoditechLogging coditechLogging, IAccSetupGLTypeService accSetupGLTypeService)
        {
            _accSetupGLTypeService = accSetupGLTypeService;
            _coditechLogging = coditechLogging;
        }
        [HttpGet]
        [Route("/AccSetupGLType/AccSetupGLTypeList")]
        [Produces(typeof(AccSetupGLTypeListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetAccSetupGLTypeList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                AccSetupGLTypeListModel list = _accSetupGLTypeService.GetAccSetupGLTypeList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AccSetupGLTypeListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupGLTypeListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupGLType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupGLTypeListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
