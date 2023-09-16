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
    public class GeneralTaxMasterController : BaseController
    {
        private readonly IGeneralTaxMasterService _generalTaxMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralTaxMasterController(ICoditechLogging coditechLogging, IGeneralTaxMasterService generalTaxMasterService)
        {
            _generalTaxMasterService = generalTaxMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralTaxMaster/GetTaxMasterList")]
        [Produces(typeof(GeneralTaxMasterListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetTaxMasterList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralTaxMasterListModel list = _generalTaxMasterService.GetTaxMasterList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralTaxMasterListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTaxMasterListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTaxMasterListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTaxMaster/CreateTaxMaster")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralTaxMasterResponse))]
        public IActionResult CreateTaxMaster([FromBody] GeneralTaxMasterModel model)
        {
            try
            {
                GeneralTaxMasterModel taxMaster = _generalTaxMasterService.CreateTaxMaster(model);
                return IsNotNull(taxMaster) ? CreateCreatedResponse(new GeneralTaxMasterResponse { GeneralTaxModel = taxMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTaxMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTaxMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTaxMaster/GetTaxMaster")]
        [HttpGet]
        [Produces(typeof(GeneralTaxMasterModel))]
        public IActionResult GetTaxMaster(short generalTaxMasterId)
        {
            try
            {
                GeneralTaxMasterModel generalTaxMasterModel = _generalTaxMasterService.GetTaxMaster(generalTaxMasterId);
                return IsNotNull(generalTaxMasterModel) ? CreateOKResponse(generalTaxMasterModel) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTaxMasterModel { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTaxMasterModel { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTaxMaster/UpdateTaxMaster")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralTaxMasterResponse))]
        public IActionResult UpdateTaxMaster([FromBody] GeneralTaxMasterModel model)
        {
            try
            {
                bool isUpdated = _generalTaxMasterService.UpdateTaxMaster(model);
                return isUpdated ? CreateOKResponse(new GeneralTaxMasterResponse { GeneralTaxModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTaxMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTaxMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTaxMaster/DeleteTaxMaster")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteTaxMaster([FromBody] ParameterModel taxMasterIds)
        {
            try
            {
                bool deleted = _generalTaxMasterService.DeleteTaxMaster(taxMasterIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}