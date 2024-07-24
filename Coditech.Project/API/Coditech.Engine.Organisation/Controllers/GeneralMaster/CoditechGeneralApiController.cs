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
    public class CoditechGeneralApiController : BaseController
    {
        private readonly ICoditechGeneralApiService _coditechGeneralApiService;
        protected readonly ICoditechLogging _coditechLogging;
        public CoditechGeneralApiController(ICoditechLogging coditechLogging, ICoditechGeneralApiService coditechApplicationSettingService)
        {
            _coditechGeneralApiService = coditechApplicationSettingService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/CoditechGeneralApi/GetCoditechApplicationSettingList")]
        [Produces(typeof(CoditechApplicationSettingListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetCoditechApplicationSettingList(string applicationCodes)
        {
            try
            {
                CoditechApplicationSettingListModel list = _coditechGeneralApiService.GetCoditechApplicationSettingList(applicationCodes);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<CoditechApplicationSettingListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechGeneralApi.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new CoditechApplicationSettingListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechGeneralApi.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new CoditechApplicationSettingListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}