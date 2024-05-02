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
    public class HospitalDoctorAllocatedOPDRoomController : BaseController
    {
        private readonly IHospitalDoctorAllocatedOPDRoomService _hospitalDoctorAllocatedOPDRoomService;
        protected readonly ICoditechLogging _coditechLogging;
        public HospitalDoctorAllocatedOPDRoomController(ICoditechLogging coditechLogging, IHospitalDoctorAllocatedOPDRoomService hospitalDoctorAllocatedOPDRoomService)
        {
            _hospitalDoctorAllocatedOPDRoomService = hospitalDoctorAllocatedOPDRoomService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/HospitalDoctorAllocatedOPDRoom/GetHospitalDoctorAllocatedOPDRoomList")]
        [Produces(typeof(HospitalDoctorAllocatedOPDRoomListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetHospitalDoctorAllocatedOPDRoomList(string selectedCentreCode, short selectedDepartmentId, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                HospitalDoctorAllocatedOPDRoomListModel list = _hospitalDoctorAllocatedOPDRoomService.GetHospitalDoctorAllocatedOPDRoomList(selectedCentreCode, selectedDepartmentId, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<HospitalDoctorAllocatedOPDRoomListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorAllocatedOPDRoomListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorAllocatedOPDRoomListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctorAllocatedOPDRoom/CreateHospitalDoctorAllocatedOPDRoom")]
        [HttpPost, ValidateModel]
        [Produces(typeof(HospitalDoctorAllocatedOPDRoomResponse))]
        public virtual IActionResult CreateHospitalDoctorAllocatedOPDRoom([FromBody] HospitalDoctorAllocatedOPDRoomModel model)
        {
            try
            {
                HospitalDoctorAllocatedOPDRoomModel hospitalDoctorAllocatedOPDRoom = _hospitalDoctorAllocatedOPDRoomService.CreateHospitalDoctorAllocatedOPDRoom(model);
                return IsNotNull(hospitalDoctorAllocatedOPDRoom) ? CreateCreatedResponse(new HospitalDoctorAllocatedOPDRoomResponse { HospitalDoctorAllocatedOPDRoomModel = hospitalDoctorAllocatedOPDRoom }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalDoctorAllocatedOPDRoomResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorAllocatedOPDRoomResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctorAllocatedOPDRoom/GetHospitalDoctorAllocatedOPDRoom")]
        [HttpGet]
        [Produces(typeof(HospitalDoctorAllocatedOPDRoomResponse))]
        public virtual IActionResult GetHospitalDoctorAllocatedOPDRoom(int hospitalDoctorAllocatedOPDRoomId)
        {
            try
            {
                HospitalDoctorAllocatedOPDRoomModel hospitalDoctorAllocatedOPDRoomModel = _hospitalDoctorAllocatedOPDRoomService.GetHospitalDoctorAllocatedOPDRoom(hospitalDoctorAllocatedOPDRoomId);
                return IsNotNull(hospitalDoctorAllocatedOPDRoomModel) ? CreateOKResponse(new HospitalDoctorAllocatedOPDRoomResponse { HospitalDoctorAllocatedOPDRoomModel = hospitalDoctorAllocatedOPDRoomModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalDoctorAllocatedOPDRoomResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorAllocatedOPDRoomResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctorAllocatedOPDRoom/UpdateHospitalDoctorAllocatedOPDRoom")]
        [HttpPut, ValidateModel]
        [Produces(typeof(HospitalDoctorAllocatedOPDRoomResponse))]
        public virtual IActionResult UpdateHospitalDoctorAllocatedOPDRoom([FromBody] HospitalDoctorAllocatedOPDRoomModel model)
        {
            try
            {
                bool isUpdated = _hospitalDoctorAllocatedOPDRoomService.UpdateHospitalDoctorAllocatedOPDRoom(model);
                return isUpdated ? CreateOKResponse(new HospitalDoctorAllocatedOPDRoomResponse { HospitalDoctorAllocatedOPDRoomModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalDoctorAllocatedOPDRoomResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorAllocatedOPDRoomResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctorAllocatedOPDRoom/DeleteHospitalDoctorAllocatedOPDRoom")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteHospitalDoctorAllocatedOPDRoom([FromBody] ParameterModel hospitalDoctorAllocatedOPDRoomIds)
        {
            try
            {
                bool deleted = _hospitalDoctorAllocatedOPDRoomService.DeleteHospitalDoctorAllocatedOPDRoom(hospitalDoctorAllocatedOPDRoomIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}