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
    public class HospitalDoctorOPDScheduleController : BaseController
    {
        private readonly IHospitalDoctorOPDScheduleService _hospitalDoctorOPDScheduleService;
        protected readonly ICoditechLogging _coditechLogging;
        public HospitalDoctorOPDScheduleController(ICoditechLogging coditechLogging, IHospitalDoctorOPDScheduleService hospitalDoctorOPDScheduleService)
        {
            _hospitalDoctorOPDScheduleService = hospitalDoctorOPDScheduleService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/HospitalDoctorOPDSchedule/GetHospitalDoctorOPDScheduleList")]
        [Produces(typeof(HospitalDoctorOPDScheduleListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetHospitalDoctorOPDScheduleList(string selectedCentreCode, short selectedDepartmentId, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                HospitalDoctorOPDScheduleListModel list = _hospitalDoctorOPDScheduleService.GetHospitalDoctorOPDScheduleList(selectedCentreCode, selectedDepartmentId, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<HospitalDoctorOPDScheduleListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorOPDSchedule.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorOPDScheduleListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorOPDSchedule.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorOPDScheduleListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctorOPDSchedule/GetHospitalDoctorOPDSchedule")]
        [HttpGet]
        [Produces(typeof(HospitalDoctorOPDScheduleResponse))]
        public virtual IActionResult GetHospitalDoctorOPDSchedule(int hospitalDoctorId, long hospitalDoctorOPDScheduleId)
        {
            try
            {
                HospitalDoctorOPDScheduleModel hospitalDoctorOPDScheduleModel = _hospitalDoctorOPDScheduleService.GetHospitalDoctorOPDSchedule(hospitalDoctorId,hospitalDoctorOPDScheduleId);
                return IsNotNull(hospitalDoctorOPDScheduleModel) ? CreateOKResponse(new HospitalDoctorOPDScheduleResponse { HospitalDoctorOPDScheduleModel = hospitalDoctorOPDScheduleModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorOPDSchedule.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalDoctorOPDScheduleResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorOPDSchedule.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorOPDScheduleResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctorOPDSchedule/UpdateHospitalDoctorOPDSchedule")]
        [HttpPut, ValidateModel]
        [Produces(typeof(HospitalDoctorOPDScheduleResponse))]
        public virtual IActionResult UpdateHospitalDoctorOPDSchedule([FromBody] HospitalDoctorOPDScheduleModel model)
        {
            try
            {
                bool isUpdated = _hospitalDoctorOPDScheduleService.UpdateHospitalDoctorOPDSchedule(model);
                return isUpdated ? CreateOKResponse(new HospitalDoctorOPDScheduleResponse { HospitalDoctorOPDScheduleModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorOPDSchedule.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalDoctorOPDScheduleResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorOPDSchedule.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorOPDScheduleResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}