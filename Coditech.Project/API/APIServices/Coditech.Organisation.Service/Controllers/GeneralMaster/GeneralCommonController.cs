using System.Diagnostics;
using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Controllers
{
    [ApiController]
    public class GeneralCommonController : BaseController
    {

        private readonly IGeneralCommonService _generalCommonService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralCommonController(ICoditechLogging coditechLogging, IGeneralCommonService generalCommonService)
        {
            _generalCommonService = generalCommonService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralCommon/GetDropdownListByCode")]
        [Produces(typeof(GeneralEnumaratorListResponse))]
        public virtual IActionResult GetDropdownListByCode(string groupCodes)
        {
            try
            {
                List<GeneralEnumaratorModel> list = _generalCommonService.GetDropdownListByCode(groupCodes);
                return IsNotNull(list) ? CreateOKResponse(new GeneralEnumaratorListResponse { GeneralEnumaratorList = list }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralCommon/SendOTP")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralMessagesResponse))]
        public virtual IActionResult SendOTP([FromBody] GeneralMessagesModel generalMessagesModel)
        {
            try
            {
                GeneralMessagesModel generalCommon = _generalCommonService.SendOTP(generalMessagesModel);
                return IsNotNull(generalCommon) ? CreateOKResponse(new GeneralMessagesResponse { GeneralMessagesModel = generalMessagesModel }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralMessages.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralMessagesResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralMessages.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralMessagesResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("/GeneralCommon/GetCoditechApplicationSettingList")]
        [Produces(typeof(CoditechApplicationSettingListResponse))]
        public virtual IActionResult GetCoditechApplicationSettingList(string applicationCodes)
        {
            try
            {
                CoditechApplicationSettingListModel list = _generalCommonService.GetCoditechApplicationSettingList(applicationCodes);
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
        [Route("/GeneralCommon/GetDomainAPIKey")]
        [Produces(typeof(StringResponse))]
        public virtual IActionResult GetDomainAPIKey(string requestKey)
        {
            try
            {
                string apiDomainkey = _generalCommonService.GetDomainAPIKey(requestKey);
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
        [Route("/GeneralCommon/GetAccountPrequisite")]
        [HttpGet]
        [Produces(typeof(AccPrequisiteResponse))]
        public virtual IActionResult GetAccountPrequisite(int balanceSheetId)
        {
            try
            {
                AccPrequisiteModel accPrequisiteModel = _generalCommonService.GetAccountPrequisite(balanceSheetId);
                return IsNotNull(accPrequisiteModel) ? CreateOKResponse(new AccPrequisiteResponse { AccPrequisiteModel = accPrequisiteModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralCountryResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCountryResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("GeneralCommon/FetchPostalCode/")]
        [HttpGet]
        [Produces(typeof(BindAddressToPostalCodeListResponse))]
        public virtual IActionResult FetchPostalCode(string postalCode)
        {
            try
            {
                List<BindAddressToPostalCodeModel> list = _generalCommonService.FetchPostalCode(postalCode);
                return IsNotNull(list) ? CreateOKResponse(new BindAddressToPostalCodeListResponse { BindAddressToPostalCodeList = list }) : CreateNoContentResponse();

            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralCountryResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCountryResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/GeneralCommon/ValidateAddress")]
        [HttpPost, ValidateModel]
        [Produces(typeof(BindAddressToPostalCodeResponse))]
        public virtual IActionResult ValidateAddress([FromBody] BindAddressToPostalCodeModel bindAddressToPostalCodeModel)
        {
            try
            {
                BindAddressToPostalCodeModel generalCommon = _generalCommonService.ValidateAddress(bindAddressToPostalCodeModel);
                return IsNotNull(generalCommon) ? CreateOKResponse(new BindAddressToPostalCodeResponse { BindAddressToPostalCodeModel = bindAddressToPostalCodeModel }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralMessages.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralMessagesResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralMessages.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralMessagesResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}