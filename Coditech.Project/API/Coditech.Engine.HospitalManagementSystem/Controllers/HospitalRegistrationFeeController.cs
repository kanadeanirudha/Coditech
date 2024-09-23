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

namespace Coditech.Engine.HospitalManagementSystem.Controllers
{
    public class HospitalRegistrationFeeController : BaseController
    {
        private readonly IHospitalRegistrationFeeService _hospitalRegistrationFeeService;
        protected readonly ICoditechLogging _coditechLogging;
        public HospitalRegistrationFeeController(ICoditechLogging coditechLogging, IHospitalRegistrationFeeService hospitalRegistrationFeeService)
        {
            _hospitalRegistrationFeeService = hospitalRegistrationFeeService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/HospitalRegistrationFee/GetHospitalRegistrationFeeList")]
        [Produces(typeof(HospitalRegistrationFeeListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetHospitalRegistrationFeeList(string selectedCentreCode, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                HospitalRegistrationFeeListModel list = _hospitalRegistrationFeeService.GetHospitalRegistrationFeeList(selectedCentreCode, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<HospitalRegistrationFeeListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalRegistrationFeeListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalRegistrationFeeListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalRegistrationFee/CreateRegistrationFee")]
        [HttpPost, ValidateModel]
        [Produces(typeof(HospitalRegistrationFeeResponse))]
        public virtual IActionResult CreateRegistrationFee([FromBody] HospitalRegistrationFeeModel model)
        {
            try
            {
                HospitalRegistrationFeeModel hospitalRegistrationFee = _hospitalRegistrationFeeService.CreateRegistrationFee(model);
                return IsNotNull(hospitalRegistrationFee) ? CreateCreatedResponse(new HospitalRegistrationFeeResponse { HospitalRegistrationFeeModel = hospitalRegistrationFee }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalRegistrationFeeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalRegistrationFeeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalRegistrationFee/GetRegistrationFee")]
        [HttpGet]
        [Produces(typeof(HospitalRegistrationFeeResponse))]
        public virtual IActionResult GetRegistrationFee(int hospitalRegistrationFeeId)
        {
            try
            {
                HospitalRegistrationFeeModel hospitalRegistrationFeeModel = _hospitalRegistrationFeeService.GetRegistrationFee(hospitalRegistrationFeeId);
                return IsNotNull(hospitalRegistrationFeeModel) ? CreateOKResponse(new HospitalRegistrationFeeResponse { HospitalRegistrationFeeModel = hospitalRegistrationFeeModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalRegistrationFeeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalRegistrationFeeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalRegistrationFee/UpdateRegistrationFee")]
        [HttpPut, ValidateModel]
        [Produces(typeof(HospitalRegistrationFeeResponse))]
        public virtual IActionResult UpdateRegistrationFee([FromBody] HospitalRegistrationFeeModel model)
        {
            try
            {
                bool isUpdated = _hospitalRegistrationFeeService.UpdateRegistrationFee(model);
                return isUpdated ? CreateOKResponse(new HospitalRegistrationFeeResponse { HospitalRegistrationFeeModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalRegistrationFeeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalRegistrationFeeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalRegistrationFee/DeleteRegistrationFee")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteRegistrationFee([FromBody] ParameterModel hospitalRegistrationFeeIds)
        {
            try
            {
                bool deleted = _hospitalRegistrationFeeService.DeleteRegistrationFee(hospitalRegistrationFeeIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}