using Coditech.API.Data;
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

namespace Coditech.Engine.HospitalManagementSystem.Controllers
{
    public class HospitalPatientAppointmentPurposeController : BaseController
    {
        private readonly IHospitalPatientAppointmentPurposeService _hospitalPatientAppointmentPurposeService;
        protected readonly ICoditechLogging _coditechLogging;
        public HospitalPatientAppointmentPurposeController(ICoditechLogging coditechLogging, IHospitalPatientAppointmentPurposeService hospitalPatientAppointmentPurposeService)
        {
            _hospitalPatientAppointmentPurposeService = hospitalPatientAppointmentPurposeService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/HospitalPatientAppointmentPurpose/GetHospitalPatientAppointmentPurposeList")]
        [Produces(typeof(HospitalPatientAppointmentPurposeListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetHospitalPatientAppointmentPurposeList(/*string selectedCentreCode, short selectedDepartmentId,*/ FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                HospitalPatientAppointmentPurposeListModel list = _hospitalPatientAppointmentPurposeService.GetHospitalPatientAppointmentPurposeList(/*selectedCentreCode, selectedDepartmentId, */filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
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

        [Route("/HospitalPatientAppointmentPurpose/CreateHospitalPatientAppointmentPurpose")]
        [HttpPost, ValidateModel]
        [Produces(typeof(HospitalPatientAppointmentPurposeResponse))]
        public virtual IActionResult CreateHospitalPatientAppointmentPurpose([FromBody] HospitalPatientAppointmentPurposeModel model)
        {
            try
            {
                HospitalPatientAppointmentPurposeModel hospitalPatientAppointmentPurpose = _hospitalPatientAppointmentPurposeService.CreateHospitalPatientAppointmentPurpose(model);
                return IsNotNull(hospitalPatientAppointmentPurpose) ? CreateCreatedResponse(new HospitalPatientAppointmentPurposeResponse { HospitalPatientAppointmentPurposeModel = hospitalPatientAppointmentPurpose }) : CreateInternalServerErrorResponse();
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

        [Route("/HospitalPatientAppointmentPurpose/GetHospitalPatientAppointmentPurpose")]
        [HttpGet]
        [Produces(typeof(HospitalPatientAppointmentPurposeResponse))]
        public virtual IActionResult GetHospitalPatientAppointmentPurpose(short hospitalPatientAppointmentPurposeId)
        {
            try
            {
                HospitalPatientAppointmentPurposeModel hospitalPatientAppointmentPurposeModel = _hospitalPatientAppointmentPurposeService.GetHospitalPatientAppointmentPurpose(hospitalPatientAppointmentPurposeId);
                return IsNotNull(hospitalPatientAppointmentPurposeModel) ? CreateOKResponse(new HospitalPatientAppointmentPurposeResponse { HospitalPatientAppointmentPurposeModel = hospitalPatientAppointmentPurposeModel }) : CreateNoContentResponse();
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

        [Route("/HospitalPatientAppointmentPurpose/UpdateHospitalPatientAppointmentPurpose")]
        [HttpPut, ValidateModel]
        [Produces(typeof(HospitalPatientAppointmentPurposeResponse))]
        public virtual IActionResult UpdateHospitalPatientAppointmentPurpose([FromBody] HospitalPatientAppointmentPurposeModel model)
        {
            try
            {
                bool isUpdated = _hospitalPatientAppointmentPurposeService.UpdateHospitalPatientAppointmentPurpose(model);
                return isUpdated ? CreateOKResponse(new HospitalPatientAppointmentPurposeResponse { HospitalPatientAppointmentPurposeModel = model }) : CreateInternalServerErrorResponse();
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

        [Route("/HospitalPatientAppointmentPurpose/DeleteHospitalPatientAppointmentPurpose")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteHospitalPatientAppointmentPurpose([FromBody] ParameterModel hospitalPatientAppointmentPurposeIds)
        {
            try
            {
                bool deleted = _hospitalPatientAppointmentPurposeService.DeleteHospitalPatientAppointmentPurpose(hospitalPatientAppointmentPurposeIds);
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