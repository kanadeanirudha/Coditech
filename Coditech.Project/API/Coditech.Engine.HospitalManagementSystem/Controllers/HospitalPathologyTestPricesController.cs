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
    public class HospitalPathologyTestPricesController : BaseController
    {
        private readonly IHospitalPathologyTestPricesService _hospitalPathologyTestPricesService;
        protected readonly ICoditechLogging _coditechLogging;
        public HospitalPathologyTestPricesController(ICoditechLogging coditechLogging, IHospitalPathologyTestPricesService hospitalPathologyTestPricesService)
        {
            _hospitalPathologyTestPricesService = hospitalPathologyTestPricesService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/HospitalPathologyTestPrices/GetHospitalPathologyTestPricesList")]
        [Produces(typeof(HospitalPathologyTestPricesListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetHospitalPathologyTestPricesList(int hospitalPathologyPriceCategoryEnumId, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                HospitalPathologyTestPricesListModel list = _hospitalPathologyTestPricesService.GetHospitalPathologyTestPricesList(hospitalPathologyPriceCategoryEnumId,filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<HospitalPathologyTestPricesListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestPricesListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestPricesListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPathologyTestPrices/CreateHospitalPathologyTestPrices")]
        [HttpPost, ValidateModel]
        [Produces(typeof(HospitalPathologyTestPricesResponse))]
        public virtual IActionResult CreateHospitalPathologyTestPrices([FromBody] HospitalPathologyTestPricesModel model)
        {
            try
            {
                HospitalPathologyTestPricesModel hospitalPathologyTestPrices = _hospitalPathologyTestPricesService.CreateHospitalPathologyTestPrices(model);
                return IsNotNull(hospitalPathologyTestPrices) ? CreateCreatedResponse(new HospitalPathologyTestPricesResponse { HospitalPathologyTestPricesModel = hospitalPathologyTestPrices }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestPricesResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestPricesResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPathologyTestPrices/GetHospitalPathologyTestPrices")]
        [HttpGet]
        [Produces(typeof(HospitalPathologyTestPricesResponse))]
        public virtual IActionResult GetHospitalPathologyTestPrices(long hospitalPathologyTestPricesId)
        {
            try
            {
                HospitalPathologyTestPricesModel hospitalPathologyTestPricesModel = _hospitalPathologyTestPricesService.GetHospitalPathologyTestPrices(hospitalPathologyTestPricesId);
                return IsNotNull(hospitalPathologyTestPricesModel) ? CreateOKResponse(new HospitalPathologyTestPricesResponse { HospitalPathologyTestPricesModel = hospitalPathologyTestPricesModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestPricesResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestPricesResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPathologyTestPrices/UpdateHospitalPathologyTestPrices")]
        [HttpPut, ValidateModel]
        [Produces(typeof(HospitalPathologyTestPricesResponse))]
        public virtual IActionResult UpdateHospitalPathologyTestPrices([FromBody] HospitalPathologyTestPricesModel model)
        {
            try
            {
                bool isUpdated = _hospitalPathologyTestPricesService.UpdateHospitalPathologyTestPrices(model);
                return isUpdated ? CreateOKResponse(new HospitalPathologyTestPricesResponse { HospitalPathologyTestPricesModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestPricesResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestPricesResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPathologyTestPrices/DeleteHospitalPathologyTestPrices")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteHospitalPathologyTestPrices([FromBody] ParameterModel hospitalPathologyTestPricesIds)
        {
            try
            {
                bool deleted = _hospitalPathologyTestPricesService.DeleteHospitalPathologyTestPrices(hospitalPathologyTestPricesIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}