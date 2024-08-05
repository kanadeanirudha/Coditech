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
    public class GeneralSmsProviderMasterController : BaseController
    {
        private readonly IGeneralSmsProviderMasterService _generalSmsProviderService;
        private readonly ICoditechLogging _coditechLogging;
        protected readonly ICoditechLogging coditechLogging;

        public GeneralSmsProviderMasterController(ICoditechLogging coditechLogging, IGeneralSmsProviderMasterService generalSmsProviderMasterService)
        {
            _generalSmsProviderService = generalSmsProviderMasterService;
            _coditechLogging  = coditechLogging; 
        }
        [HttpGet]
        [Route("/GeneralSmsProviderMaster/GetSmsProviderList")]
        [Produces(typeof(GeneralSmsProviderListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]   
        public virtual IActionResult GetSmsProviderList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int PageIndex, int PazeSize )
        {
            try
            {
                GeneralSmsProviderListModel list = _generalSmsProviderService.GetSmsProviderList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), PageIndex, PazeSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralSmsProviderListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex) 
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralSmsProviderListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralSmsProviderListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/GeneralSmsProviderMaster/CreateSmsProvider")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralSmsProviderListResponse))]

        public virtual IActionResult CreateSmsProvider([FromBody] GeneralSmsProviderModel model)
        {
            try
            {
                GeneralSmsProviderModel SmsProviderMaster = _generalSmsProviderService.CreateSmsProvider(model);
                return IsNotNull(SmsProviderMaster) ? CreateCreatedResponse(new GeneralSmsProviderResponse { GeneralSmsProviderModel = SmsProviderMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralSmsProviderResponse {  HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralSmsProviderResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/GeneralSmsProviderMaster/GetSmsProvider")]
        [HttpGet]
        [Produces(typeof(GeneralSmsProviderResponse))]
        public virtual IActionResult GetSmsProvider(short generalSmsProviderId)
        {
        
            try
            {
                GeneralSmsProviderModel generalSmsProviderMasterModel = _generalSmsProviderService.GetSmsProvider(generalSmsProviderId);
                return IsNotNull(generalSmsProviderMasterModel) ? CreateOKResponse(new GeneralSmsProviderResponse { GeneralSmsProviderModel = generalSmsProviderMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralSmsProviderResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralSmsProviderResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralSmsProviderMaster/UpdateSmsProvider")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralSmsProviderResponse))]
        public virtual IActionResult UpdateSmsProvider([FromBody] GeneralSmsProviderModel model)
        {
            try
            {
                bool isUpdated = _generalSmsProviderService.UpdateSmsProvider(model);
                return isUpdated ? CreateOKResponse(new GeneralSmsProviderResponse { GeneralSmsProviderModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralSmsProviderResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralSmsProviderResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralSmsProviderMaster/DeleteSmsProvider")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteSmsProvider([FromBody] ParameterModel SmsProviderIds)
        {
            try
            {
                bool deleted = _generalSmsProviderService.DeleteSmsProvider(SmsProviderIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SmsProviderMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }

}

