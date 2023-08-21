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
    public class GeneralTaxGroupMasterController : BaseController
    {
        private readonly IGeneralTaxGroupMasterService _generalTaxGroupMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralTaxGroupMasterController(ICoditechLogging coditechLogging, IGeneralTaxGroupMasterService generalTaxGroupMasterService)
        {
            _generalTaxGroupMasterService = generalTaxGroupMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralTaxGroupMaster/GetTaxGroupMasterList")]
        [Produces(typeof(GeneralTaxGroupMasterListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetTaxGroupMasterList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralTaxGroupMasterListModel list = _generalTaxGroupMasterService.GetTaxGroupMasterList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralTaxGroupMasterListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupMasterListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupMasterListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTaxGroupMaster/CreateTaxGroupMaster")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralTaxGroupMasterResponse))]
        public IActionResult CreateTaxGroupMaster([FromBody] GeneralTaxGroupMasterModel model)
        {
            try
            {
                GeneralTaxGroupMasterModel taxGroupMaster = _generalTaxGroupMasterService.CreateTaxGroupMaster(model);
                return IsNotNull(taxGroupMaster) ? CreateCreatedResponse(new GeneralTaxGroupMasterResponse { GeneralTaxGroupMasterModel = taxGroupMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupMasterResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTaxGroupMaster/GetTaxGroupMaster")]
        [HttpGet]
        [Produces(typeof(GeneralTaxGroupMasterModel))]
        public IActionResult GetTaxGroupMaster(short generalTaxGroupMasterId)
        {
            try
            {
                GeneralTaxGroupMasterModel generalTaxGroupMasterModel = _generalTaxGroupMasterService.GetTaxGroupMaster(generalTaxGroupMasterId);
                return IsNotNull(generalTaxGroupMasterModel) ? CreateOKResponse(generalTaxGroupMasterModel) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupMasterModel { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupMasterModel { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTaxGroupMaster/UpdateTaxGroupMaster")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralTaxGroupMasterResponse))]
        public IActionResult UpdateTaxGroupMaster([FromBody] GeneralTaxGroupMasterModel model)
        {
            try
            {
                bool isUpdated = _generalTaxGroupMasterService.UpdateTaxGroupMaster(model);
                return isUpdated ? CreateOKResponse(new GeneralTaxGroupMasterResponse { GeneralTaxGroupMasterModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupMasterResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTaxGroupMaster/DeleteTaxGroupMaster")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteTaxGroupMaster([FromBody] ParameterModel taxGroupMasterIds)
        {
            try
            {
                bool deleted = _generalTaxGroupMasterService.DeleteTaxGroupMaster(taxGroupMasterIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}