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
    public class HospitalPatientTypeController : BaseController
    {
        private readonly IHospitalPatientTypeService _hospitalPatientTypeService;
        protected readonly ICoditechLogging _coditechLogging;
        public HospitalPatientTypeController(ICoditechLogging coditechLogging, IHospitalPatientTypeService hospitalPatientTypeService)
        {
            _hospitalPatientTypeService = hospitalPatientTypeService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/HospitalPatientType/GetHospitalPatientTypeList")]
        [Produces(typeof(HospitalPatientTypeListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetHospitalPatientTypeList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                HospitalPatientTypeListModel list = _hospitalPatientTypeService.GetHospitalPatientTypeList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<HospitalPatientTypeListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientTypeListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientTypeListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPatientType/CreateHospitalPatientType")]
        [HttpPost, ValidateModel]
        [Produces(typeof(HospitalPatientTypeResponse))]
        public virtual IActionResult CreateHospitalPatientType([FromBody] HospitalPatientTypeModel model)
        {
            try
            {
                HospitalPatientTypeModel hospitalPatientType = _hospitalPatientTypeService.CreateHospitalPatientType(model);
                return IsNotNull(hospitalPatientType) ? CreateCreatedResponse(new HospitalPatientTypeResponse { HospitalPatientTypeModel = hospitalPatientType }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPatientTypeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientTypeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPatientType/GetHospitalPatientType")]
        [HttpGet]
        [Produces(typeof(HospitalPatientTypeResponse))]
        public virtual IActionResult GetHospitalPatientType(byte hospitalPatientTypeId)
        {
            try
            {
                HospitalPatientTypeModel hospitalPatientTypeModel = _hospitalPatientTypeService.GetHospitalPatientType(hospitalPatientTypeId);
                return IsNotNull(hospitalPatientTypeModel) ? CreateOKResponse(new HospitalPatientTypeResponse { HospitalPatientTypeModel = hospitalPatientTypeModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPatientTypeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientTypeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPatientType/UpdateHospitalPatientType")]
        [HttpPut, ValidateModel]
        [Produces(typeof(HospitalPatientTypeResponse))]
        public virtual IActionResult UpdateHospitalPatientType([FromBody] HospitalPatientTypeModel model)
        {
            try
            {
                bool isUpdated = _hospitalPatientTypeService.UpdateHospitalPatientType(model);
                return isUpdated ? CreateOKResponse(new HospitalPatientTypeResponse { HospitalPatientTypeModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPatientTypeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPatientTypeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPatientType/DeleteHospitalPatientType")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteHospitalPatientType([FromBody] ParameterModel hospitalPatientTypeIds)
        {
            try
            {
                bool deleted = _hospitalPatientTypeService.DeleteHospitalPatientType(hospitalPatientTypeIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}