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
    public class HospitalDoctorLeaveScheduleController : BaseController
    {
        private readonly IHospitalDoctorLeaveScheduleService _hospitalDoctorLeaveScheduleService;
        protected readonly ICoditechLogging _coditechLogging;
        public HospitalDoctorLeaveScheduleController(ICoditechLogging coditechLogging, IHospitalDoctorLeaveScheduleService hospitalDoctorLeaveScheduleService)
        {
            _hospitalDoctorLeaveScheduleService = hospitalDoctorLeaveScheduleService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/HospitalDoctorLeaveSchedule/GetHospitalDoctorLeaveScheduleList")]
        [Produces(typeof(HospitalDoctorLeaveScheduleListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetHospitalDoctorLeaveScheduleList(string selectedCentreCode, short selectedDepartmentId, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                HospitalDoctorLeaveScheduleListModel list = _hospitalDoctorLeaveScheduleService.GetHospitalDoctorLeaveScheduleList(selectedCentreCode, selectedDepartmentId, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<HospitalDoctorLeaveScheduleListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorLeaveScheduleListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorLeaveScheduleListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctorLeaveSchedule/CreateHospitalDoctorLeaveSchedule")]
        [HttpPost, ValidateModel]
        [Produces(typeof(HospitalDoctorLeaveScheduleResponse))]
        public virtual IActionResult CreateHospitalDoctorLeaveSchedule([FromBody] HospitalDoctorLeaveScheduleModel model)
        {
            try
            {
                HospitalDoctorLeaveScheduleModel hospitalDoctorLeaveSchedule = _hospitalDoctorLeaveScheduleService.CreateHospitalDoctorLeaveSchedule(model);
                return IsNotNull(hospitalDoctorLeaveSchedule) ? CreateCreatedResponse(new HospitalDoctorLeaveScheduleResponse { HospitalDoctorLeaveScheduleModel = hospitalDoctorLeaveSchedule }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalDoctorLeaveScheduleResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorLeaveScheduleResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctorLeaveSchedule/GetHospitalDoctorLeaveSchedule")]
        [HttpGet]
        [Produces(typeof(HospitalDoctorLeaveScheduleResponse))]
        public virtual IActionResult GetHospitalDoctorLeaveSchedule(long hospitalDoctorLeaveScheduleId)
        {
            try
            {
                HospitalDoctorLeaveScheduleModel hospitalDoctorLeaveScheduleModel = _hospitalDoctorLeaveScheduleService.GetHospitalDoctorLeaveSchedule(hospitalDoctorLeaveScheduleId);
                return IsNotNull(hospitalDoctorLeaveScheduleModel) ? CreateOKResponse(new HospitalDoctorLeaveScheduleResponse { HospitalDoctorLeaveScheduleModel = hospitalDoctorLeaveScheduleModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalDoctorLeaveScheduleResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorLeaveScheduleResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctorLeaveSchedule/UpdateHospitalDoctorLeaveSchedule")]
        [HttpPut, ValidateModel]
        [Produces(typeof(HospitalDoctorLeaveScheduleResponse))]
        public virtual IActionResult UpdateHospitalDoctorLeaveSchedule([FromBody] HospitalDoctorLeaveScheduleModel model)
        {
            try
            {
                bool isUpdated = _hospitalDoctorLeaveScheduleService.UpdateHospitalDoctorLeaveSchedule(model);
                return isUpdated ? CreateOKResponse(new HospitalDoctorLeaveScheduleResponse { HospitalDoctorLeaveScheduleModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalDoctorLeaveScheduleResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorLeaveScheduleResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctorLeaveSchedule/DeleteHospitalDoctorLeaveSchedule")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteHospitalDoctorLeaveSchedule([FromBody] ParameterModel hospitalDoctorLeaveScheduleIds)
        {
            try
            {
                bool deleted = _hospitalDoctorLeaveScheduleService.DeleteHospitalDoctorLeaveSchedule(hospitalDoctorLeaveScheduleIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}