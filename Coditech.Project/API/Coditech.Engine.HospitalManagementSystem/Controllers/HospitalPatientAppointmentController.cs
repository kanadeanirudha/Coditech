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
    public class HospitalPatientAppointmentController : BaseController
    {
        private readonly IHospitalPatientAppointmentService _hospitalPatientAppointmentService;
        protected readonly ICoditechLogging _coditechLogging;
        public HospitalPatientAppointmentController(ICoditechLogging coditechLogging, IHospitalPatientAppointmentService hospitalPatientAppointmentService)
        {
            _hospitalPatientAppointmentService = hospitalPatientAppointmentService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/HospitalPatientAppointment/GetHospitalPatientAppointmentList")]
        [Produces(typeof(HospitalPatientAppointmentListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetHospitalPatientAppointmentList(string selectedCentreCode, short selectedDepartmentId, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                HospitalPatientAppointmentListModel list = _hospitalPatientAppointmentService.GetHospitalPatientAppointmentList(selectedCentreCode, selectedDepartmentId, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<HospitalPatientAppointmentListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPatientAppointment/CreateHospitalPatientAppointment")]
        [HttpPost, ValidateModel]
        [Produces(typeof(HospitalPatientAppointmentResponse))]
        public virtual IActionResult CreateHospitalPatientAppointment([FromBody] HospitalPatientAppointmentModel model)
        {
            try
            {
                HospitalPatientAppointmentModel hospitalPatientAppointment = _hospitalPatientAppointmentService.CreateHospitalPatientAppointment(model);
                return IsNotNull(hospitalPatientAppointment) ? CreateCreatedResponse(new HospitalPatientAppointmentResponse { HospitalPatientAppointmentModel = hospitalPatientAppointment }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPatientAppointment/GetHospitalPatientAppointment")]
        [HttpGet]
        [Produces(typeof(HospitalPatientAppointmentResponse))]
        public virtual IActionResult GetHospitalPatientAppointment(long hospitalPatientAppointmentId)
        {
            try
            {
                HospitalPatientAppointmentModel hospitalPatientAppointmentModel = _hospitalPatientAppointmentService.GetHospitalPatientAppointment(hospitalPatientAppointmentId);
                return IsNotNull(hospitalPatientAppointmentModel) ? CreateOKResponse(new HospitalPatientAppointmentResponse { HospitalPatientAppointmentModel = hospitalPatientAppointmentModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPatientAppointment/UpdateHospitalPatientAppointment")]
        [HttpPut, ValidateModel]
        [Produces(typeof(HospitalPatientAppointmentResponse))]
        public virtual IActionResult UpdateHospitalPatientAppointment([FromBody] HospitalPatientAppointmentModel model)
        {
            try
            {
                bool isUpdated = _hospitalPatientAppointmentService.UpdateHospitalPatientAppointment(model);
                return isUpdated ? CreateOKResponse(new HospitalPatientAppointmentResponse { HospitalPatientAppointmentModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientAppointmentResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPatientAppointment/DeleteHospitalPatientAppointment")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteHospitalPatientAppointment([FromBody] ParameterModel hospitalPatientAppointmentIds)
        {
            try
            {
                bool deleted = _hospitalPatientAppointmentService.DeleteHospitalPatientAppointment(hospitalPatientAppointmentIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientAppointment.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}