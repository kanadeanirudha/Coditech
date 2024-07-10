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
    public class HospitalPatientRegistrationController : BaseController
    {
        private readonly IHospitalPatientRegistrationService _hospitalPatientRegistrationService;
        protected readonly ICoditechLogging _coditechLogging;
        public HospitalPatientRegistrationController(ICoditechLogging coditechLogging, IHospitalPatientRegistrationService hospitalPatientRegistrationService)
        {
            _hospitalPatientRegistrationService = hospitalPatientRegistrationService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/HospitalPatientRegistration/GetPatientRegistrationList")]
        [Produces(typeof(HospitalPatientRegistrationListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetPatientRegistrationList(string selectedCentreCode, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                HospitalPatientRegistrationListModel list = _hospitalPatientRegistrationService.GetPatientRegistrationList(selectedCentreCode, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<HospitalPatientRegistrationListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientRegistration.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientRegistrationListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientRegistration.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientRegistrationListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPatientRegistration/GetPatientRegistrationOtherDetail")]
        [HttpGet]
        [Produces(typeof(HospitalPatientRegistrationResponse))]
        public virtual IActionResult GetPatientRegistrationOtherDetail(long hospitalPatientRegistrationId)
        {
            try
            {
                HospitalPatientRegistrationModel hospitalPatientRegistrationModel = _hospitalPatientRegistrationService.GetPatientRegistrationOtherDetail(hospitalPatientRegistrationId);
                return IsNotNull(hospitalPatientRegistrationModel) ? CreateOKResponse(new HospitalPatientRegistrationResponse { HospitalPatientRegistrationModel = hospitalPatientRegistrationModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientRegistration.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPatientRegistrationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientRegistration.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientRegistrationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPatientRegistration/DeletePatientRegistration")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeletePatientRegistration([FromBody] ParameterModel hospitalPatientRegistrationIds)
        {
            try
            {
                bool deleted = _hospitalPatientRegistrationService.DeletePatientRegistration(hospitalPatientRegistrationIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientRegistration.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientRegistration.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}