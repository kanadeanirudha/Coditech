using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Coditech.Common.Logger;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpGet]
        [Route("/CoditechGeneralApi/GetDomainAPIKey")]
        [Produces(typeof(StringResponse))]
        public virtual IActionResult GetDomainAPIKey(string requestKey)
        {
            try
            {
                string apiDomainkey = _coditechGeneralApiService.GetDomainAPIKey(requestKey);
                StringResponse response = new StringResponse() { Response = apiDomainkey };
                string data = ApiHelper.ToJson(response);
                return !string.IsNullOrEmpty(apiDomainkey) ? CreateOKResponse<StringResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechGeneralApi.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new StringResponse { Response = "", ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CoditechGeneralApi.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new StringResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}