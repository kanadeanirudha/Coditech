using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Controllers
{
    public class GeneralPersonFollowUpController : BaseController
    {
        private readonly IGeneralPersonFollowUpService _generalPersonFollowUpService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralPersonFollowUpController(ICoditechLogging coditechLogging, IGeneralPersonFollowUpService generalPersonFollowUpService)
        {
            _generalPersonFollowUpService = generalPersonFollowUpService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralPersonFollowUp/GetPersonFollowUpList")]
        [Produces(typeof(GeneralPersonFollowUpListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetPersonFollowUpList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralPersonFollowUpListModel list = _generalPersonFollowUpService.GetPersonFollowUpList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralPersonFollowUpListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPersonFollowUpListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPersonFollowUpListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPersonFollowUp/CreatePersonFollowUp")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralPersonFollowUpResponse))]
        public virtual IActionResult CreatePersonFollowUp([FromBody] GeneralPersonFollowUpModel model)
        {
            try
            {
                GeneralPersonFollowUpModel PersonFollowUp = _generalPersonFollowUpService.CreatePersonFollowUp(model);
                return IsNotNull(PersonFollowUp) ? CreateCreatedResponse(new GeneralPersonFollowUpResponse { GeneralPersonFollowUpModel = PersonFollowUp }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPersonFollowUpResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPersonFollowUpResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPersonFollowUp/GetPersonFollowUp")]
        [HttpGet]
        [Produces(typeof(GeneralPersonFollowUpResponse))]
        public virtual IActionResult GetPersonFollowUp(long generalPersonFollowUpId)
        {
            try
            {
                GeneralPersonFollowUpModel generalPersonFollowUpModel = _generalPersonFollowUpService.GetPersonFollowUp(generalPersonFollowUpId);
                return IsNotNull(generalPersonFollowUpModel) ? CreateOKResponse(new GeneralPersonFollowUpResponse { GeneralPersonFollowUpModel = generalPersonFollowUpModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPersonFollowUpResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPersonFollowUpResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPersonFollowUp/UpdatePersonFollowUp")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralPersonFollowUpResponse))]
        public virtual IActionResult UpdatePersonFollowUp([FromBody] GeneralPersonFollowUpModel model)
        {
            try
            {
                bool isUpdated = _generalPersonFollowUpService.UpdatePersonFollowUp(model);
                return isUpdated ? CreateOKResponse(new GeneralPersonFollowUpResponse { GeneralPersonFollowUpModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPersonFollowUpResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPersonFollowUpResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPersonFollowUp/DeletePersonFollowUp")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeletePersonFollowUp([FromBody] ParameterModel PersonFollowUpIds)
        {
            try
            {
                bool deleted = _generalPersonFollowUpService.DeletePersonFollowUp(PersonFollowUpIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}