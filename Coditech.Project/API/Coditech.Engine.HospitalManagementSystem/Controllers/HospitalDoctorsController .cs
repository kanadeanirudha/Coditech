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
    public class HospitalDoctorsController : BaseController
    {
        private readonly IHospitalDoctorsService _hospitalDoctorsService;
        protected readonly ICoditechLogging _coditechLogging;
        public HospitalDoctorsController(ICoditechLogging coditechLogging, IHospitalDoctorsService hospitalDoctorsService)
        {
            _hospitalDoctorsService = hospitalDoctorsService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/HospitalDoctors/GetHospitalDoctorsList")]
        [Produces(typeof(HospitalDoctorsListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetHospitalDoctorsList(string selectedCentreCode, short selectedDepartmentId, bool isAssociated, ExpandCollection expand, FilterCollection filter, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                HospitalDoctorsListModel list = _hospitalDoctorsService.GetHospitalDoctorsList(selectedCentreCode, selectedDepartmentId, isAssociated, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<HospitalDoctorsListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorsListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorsListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctors/CreateHospitalDoctors")]
        [HttpPost, ValidateModel]
        [Produces(typeof(HospitalDoctorsResponse))]
        public virtual IActionResult CreateHospitalDoctors([FromBody] HospitalDoctorsModel model)
        {
            try
            {
                HospitalDoctorsModel hospitalDoctors = _hospitalDoctorsService.CreateHospitalDoctors(model);
                return IsNotNull(hospitalDoctors) ? CreateCreatedResponse(new HospitalDoctorsResponse { HospitalDoctorsModel = hospitalDoctors }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalDoctorsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctors/GetHospitalDoctors")]
        [HttpGet]
        [Produces(typeof(HospitalDoctorsResponse))]
        public virtual IActionResult GetHospitalDoctors(long hospitalDoctorId)
        {
            try
            {
                HospitalDoctorsModel hospitalDoctorsModel = _hospitalDoctorsService.GetHospitalDoctors(hospitalDoctorId);
                return IsNotNull(hospitalDoctorsModel) ? CreateOKResponse(new HospitalDoctorsResponse { HospitalDoctorsModel = hospitalDoctorsModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalDoctorsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctors/UpdateHospitalDoctors")]
        [HttpPut, ValidateModel]
        [Produces(typeof(HospitalDoctorsResponse))]
        public virtual IActionResult UpdateHospitalDoctors([FromBody] HospitalDoctorsModel model)
        {
            try
            {
                bool isUpdated = _hospitalDoctorsService.UpdateHospitalDoctors(model);
                return isUpdated ? CreateOKResponse(new HospitalDoctorsResponse { HospitalDoctorsModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalDoctorsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctors/DeleteHospitalDoctors")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteHospitalDoctors([FromBody] ParameterModel hospitalDoctorIds)
        {
            try
            {
                bool deleted = _hospitalDoctorsService.DeleteHospitalDoctors(hospitalDoctorIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}