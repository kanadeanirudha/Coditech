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

namespace Coditech.API.Controllers
{
    public class GeneralMeasurementUnitMasterController : BaseController
    {
        private readonly IGeneralMeasurementUnitMasterService _generalMeasurementUnitMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralMeasurementUnitMasterController(ICoditechLogging coditechLogging, IGeneralMeasurementUnitMasterService generalMeasurementUnitMasterService)
        {
            _generalMeasurementUnitMasterService = generalMeasurementUnitMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralMeasurementUnitMaster/GetMeasurementUnitList")]
        [Produces(typeof(GeneralMeasurementUnitListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetMeasurementUnitList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralMeasurementUnitListModel list = _generalMeasurementUnitMasterService.GetMeasurementUnitList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralMeasurementUnitListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralMeasurementUnitListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralMeasurementUnitListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralMeasurementUnitMaster/CreateMeasurementUnit")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralMeasurementUnitResponse))]
        public virtual IActionResult CreateMeasurementUnit([FromBody] GeneralMeasurementUnitModel model)
        {
            try
            {
                GeneralMeasurementUnitModel MeasurementUnitMaster = _generalMeasurementUnitMasterService.CreateMeasurementUnit(model);
                return IsNotNull(MeasurementUnitMaster) ? CreateCreatedResponse(new GeneralMeasurementUnitResponse { GeneralMeasurementUnitModel = MeasurementUnitMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralMeasurementUnitResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralMeasurementUnitResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralMeasurementUnitMaster/GetMeasurementUnit")]
        [HttpGet]
        [Produces(typeof(GeneralMeasurementUnitResponse))]
        public virtual IActionResult GetMeasurementUnit(short generalMeasurementUnitMasterId)
        {
            try
            {
                GeneralMeasurementUnitModel generalMeasurementUnitMasterModel = _generalMeasurementUnitMasterService.GetMeasurementUnit(generalMeasurementUnitMasterId);
                return IsNotNull(generalMeasurementUnitMasterModel) ? CreateOKResponse(new GeneralMeasurementUnitResponse { GeneralMeasurementUnitModel = generalMeasurementUnitMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralMeasurementUnitResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralMeasurementUnitResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralMeasurementUnitMaster/UpdateMeasurementUnit")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralMeasurementUnitResponse))]
        public virtual IActionResult UpdateMeasurementUnit([FromBody] GeneralMeasurementUnitModel model)
        {
            try
            {
                bool isUpdated = _generalMeasurementUnitMasterService.UpdateMeasurementUnit(model);
                return isUpdated ? CreateOKResponse(new GeneralMeasurementUnitResponse { GeneralMeasurementUnitModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralMeasurementUnitResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralMeasurementUnitResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralMeasurementUnitMaster/DeleteMeasurementUnit")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteMeasurementUnit([FromBody] ParameterModel MeasurementUnitIds)
        {
            try
            {
                bool deleted = _generalMeasurementUnitMasterService.DeleteMeasurementUnit(MeasurementUnitIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.MeasurementUnitMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}