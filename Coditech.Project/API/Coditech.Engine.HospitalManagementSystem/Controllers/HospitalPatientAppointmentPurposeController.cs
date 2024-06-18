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
    public class HospitalPatientAppointmentPurposeController : BaseController
    {
        private readonly IHospitalPatientAppointmentPurposeService _hospitalPatientAppointmentPurposeService;
        protected readonly ICoditechLogging _coditechLogging;
        public HospitalPatientAppointmentPurposeController(ICoditechLogging coditechLogging, IHospitalPatientAppointmentPurposeService hospitalPatientAppointmentPurposeMasterService)
        {
            _hospitalPatientAppointmentPurposeService = hospitalPatientAppointmentPurposeMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/HospitalPatientAppointmentPurposeMaster/HospitalPatientAppointmentPurposeList")]
        [Produces(typeof(HospitalPatientAppointmentPurposeListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetHospitalPatientAppointmentPurposeList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                HospitalPatientAppointmentPurposeListModel list = _hospitalPatientAppointmentPurposeService.GetHospitalPatientAppointmentPurposeList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<HospitalPatientAppointmentPurposeListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentPurposeListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentPurposeListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPatientAppointmentPurposeMaster/CreateHospitalPatientAppointmentPurpose")]
        [HttpPost, ValidateModel]
        [Produces(typeof(HospitalPatientAppointmentPurposeResponse))]
        public virtual IActionResult CreateHospitalPatientAppointmentPurpose([FromBody] HospitalPatientAppointmentPurposeModel model)
        {
            try
            {
                HospitalPatientAppointmentPurposeModel hospitalPatientAppointmentPurposeMaster = _hospitalPatientAppointmentPurposeService.CreateHospitalPatientAppointmentPurpose(model);
                return IsNotNull(hospitalPatientAppointmentPurposeMaster) ? CreateCreatedResponse(new HospitalPatientAppointmentPurposeResponse { hospitalPatientAppointmentPurposeModel = hospitalPatientAppointmentPurposeMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentPurposeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentPurposeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPatientAppointmentPurposeMaster/GetHospitalPatientAppointmentPurpose")]
        [HttpGet]
        [Produces(typeof(HospitalPatientAppointmentPurposeResponse))]
        public virtual IActionResult GetHospitalPatientAppointmentPurpose(short HospitalPatientAppointmentPurposeId)
        {
            try
            {
                HospitalPatientAppointmentPurposeModel hospitalPatientAppointmentPurposeModel = _hospitalPatientAppointmentPurposeService.GetHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeId);
                return IsNotNull(hospitalPatientAppointmentPurposeModel) ? CreateOKResponse(new HospitalPatientAppointmentPurposeResponse { hospitalPatientAppointmentPurposeModel = hospitalPatientAppointmentPurposeModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentPurposeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentPurposeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPatientAppointmentPurposeMaster/UpdateHospitalPatientAppointmentPurpose")]
        [HttpPut, ValidateModel]
        [Produces(typeof(HospitalPatientAppointmentPurposeResponse))]
        public virtual IActionResult UpdateHospitalPatientAppointmentPurpose([FromBody] HospitalPatientAppointmentPurposeModel model)
        {
            try
            {
                bool isUpdated = _hospitalPatientAppointmentPurposeService.UpdateHospitalPatientAppointmentPurpose(model);
                return isUpdated ? CreateOKResponse(new HospitalPatientAppointmentPurposeResponse { hospitalPatientAppointmentPurposeModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentPurposeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentPurposeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPatientAppointmentPurposeMaster/DeleteHospitalPatientAppointmentPurpose")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteHospitalPatientAppointmentPurpose([FromBody] ParameterModel HospitalPatientAppointmentPurposeId)
        {
            try
            {
                bool deleted = _hospitalPatientAppointmentPurposeService.DeleteHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeId);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointmentPurpose.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
