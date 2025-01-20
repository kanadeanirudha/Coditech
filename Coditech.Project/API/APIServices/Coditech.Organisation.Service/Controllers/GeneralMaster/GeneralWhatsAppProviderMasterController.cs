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
    public class GeneralWhatsAppProviderMasterController : BaseController
    {
        private readonly IGeneralWhatsAppProviderMasterService _generalWhatsAppProviderService;
        private readonly ICoditechLogging _coditechLogging;
        protected readonly ICoditechLogging coditechLogging;

        public GeneralWhatsAppProviderMasterController(ICoditechLogging coditechLogging, IGeneralWhatsAppProviderMasterService generalWhatsAppProviderMasterService)
        {
            _generalWhatsAppProviderService = generalWhatsAppProviderMasterService;
            _coditechLogging = coditechLogging;
        }
        [HttpGet]
        [Route("/GeneralWhatsAppProviderMaster/GetWhatsAppProviderList")]
        [Produces(typeof(GeneralWhatsAppProviderListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]

        public virtual IActionResult GetWhatsAppProviderList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int PageIndex, int PazeSize)
        {
            try
            {
                GeneralWhatsAppProviderListModel list = _generalWhatsAppProviderService.GetWhatsAppProviderList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), PageIndex, PazeSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralWhatsAppProviderListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralWhatsAppProviderListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralWhatsAppProviderListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/GeneralWhatsAppProviderMaster/CreateWhatsAppProvider")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralWhatsAppProviderListResponse))]

        public virtual IActionResult CreateWhatsAppProvider([FromBody] GeneralWhatsAppProviderModel model)
        {
            try
            {
                GeneralWhatsAppProviderModel WhatsAppProviderMaster = _generalWhatsAppProviderService.CreateWhatsAppProvider(model);
                return IsNotNull(WhatsAppProviderMaster) ? CreateCreatedResponse(new GeneralWhatsAppProviderResponse { GeneralWhatsAppProviderModel = WhatsAppProviderMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralWhatsAppProviderResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralWhatsAppProviderResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralWhatsAppProviderMaster/GetWhatsAppProvider")]
        [HttpGet]
        [Produces(typeof(GeneralWhatsAppProviderResponse))]
        public virtual IActionResult GetWhatsAppProvider(short generalWhatsAppProviderId)
        {

            try
            {
                GeneralWhatsAppProviderModel generalWhatsAppProviderMasterModel = _generalWhatsAppProviderService.GetWhatsAppProvider(generalWhatsAppProviderId);
                return IsNotNull(generalWhatsAppProviderMasterModel) ? CreateOKResponse(new GeneralWhatsAppProviderResponse { GeneralWhatsAppProviderModel = generalWhatsAppProviderMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralWhatsAppProviderResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralWhatsAppProviderResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralWhatsAppProviderMaster/UpdateWhatsAppProvider")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralWhatsAppProviderResponse))]
        public virtual IActionResult UpdateWhatsAppProvider([FromBody] GeneralWhatsAppProviderModel model)
        {
            try
            {
                bool isUpdated = _generalWhatsAppProviderService.UpdateWhatsAppProvider(model);
                return isUpdated ? CreateOKResponse(new GeneralWhatsAppProviderResponse { GeneralWhatsAppProviderModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralWhatsAppProviderResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralWhatsAppProviderResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/GeneralWhatsAppProviderMaster/DeleteWhatsAppProvider")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteWhatsAppProvider([FromBody] ParameterModel WhatsAppProviderIds)
        {
            try
            {
                bool deleted = _generalWhatsAppProviderService.DeleteWhatsAppProvider(WhatsAppProviderIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Error);
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.WhatsAppProviderMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }


    }
}
