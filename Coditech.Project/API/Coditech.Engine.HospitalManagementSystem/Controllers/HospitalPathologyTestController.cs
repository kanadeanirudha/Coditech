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
    public class HospitalPathologyTestController : BaseController
    {
        private readonly IHospitalPathologyTestService _hospitalPathologyTestService;
        protected readonly ICoditechLogging _coditechLogging;
        public HospitalPathologyTestController(ICoditechLogging coditechLogging, IHospitalPathologyTestService hospitalPathologyTestService)
        {
            _hospitalPathologyTestService = hospitalPathologyTestService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/HospitalPathologyTest/GetHospitalPathologyTestList")]
        [Produces(typeof(HospitalPathologyTestListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetHospitalPathologyTestList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                HospitalPathologyTestListModel list = _hospitalPathologyTestService.GetHospitalPathologyTestList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<HospitalPathologyTestListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPathologyTest/CreateHospitalPathologyTest")]
        [HttpPost, ValidateModel]
        [Produces(typeof(HospitalPathologyTestResponse))]
        public virtual IActionResult CreateHospitalPathologyTest([FromBody] HospitalPathologyTestModel model)
        {
            try
            {
                HospitalPathologyTestModel hospitalPathologyTest = _hospitalPathologyTestService.CreateHospitalPathologyTest(model);
                return IsNotNull(hospitalPathologyTest) ? CreateCreatedResponse(new HospitalPathologyTestResponse { HospitalPathologyTestModel = hospitalPathologyTest }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPathologyTest/GetHospitalPathologyTest")]
        [HttpGet]
        [Produces(typeof(HospitalPathologyTestResponse))]
        public virtual IActionResult GetHospitalPathologyTest(long hospitalPathologyTestId)
        {
            try
            {
                HospitalPathologyTestModel hospitalPathologyTestModel = _hospitalPathologyTestService.GetHospitalPathologyTest(hospitalPathologyTestId);
                return IsNotNull(hospitalPathologyTestModel) ? CreateOKResponse(new HospitalPathologyTestResponse { HospitalPathologyTestModel = hospitalPathologyTestModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPathologyTest/UpdateHospitalPathologyTest")]
        [HttpPut, ValidateModel]
        [Produces(typeof(HospitalPathologyTestResponse))]
        public virtual IActionResult UpdateHospitalPathologyTest([FromBody] HospitalPathologyTestModel model)
        {
            try
            {
                bool isUpdated = _hospitalPathologyTestService.UpdateHospitalPathologyTest(model);
                return isUpdated ? CreateOKResponse(new HospitalPathologyTestResponse { HospitalPathologyTestModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPathologyTest/DeleteHospitalPathologyTest")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteHospitalPathologyTest([FromBody] ParameterModel hospitalPathologyTestIds)
        {
            try
            {
                bool deleted = _hospitalPathologyTestService.DeleteHospitalPathologyTest(hospitalPathologyTestIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTest.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}