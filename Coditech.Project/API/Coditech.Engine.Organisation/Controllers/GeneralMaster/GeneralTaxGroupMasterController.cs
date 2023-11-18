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
        [Produces(typeof(GeneralTaxGroupListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetTaxGroupMasterList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralTaxGroupMasterListModel list = _generalTaxGroupMasterService.GetTaxGroupMasterList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralTaxGroupListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTaxGroupMaster/CreateTaxGroupMaster")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralTaxGroupResponse))]
        public IActionResult CreateTaxGroupMaster([FromBody] GeneralTaxGroupModel model)
        {
            try
            {
                GeneralTaxGroupModel taxGroupMaster = _generalTaxGroupMasterService.CreateTaxGroupMaster(model);
                return IsNotNull(taxGroupMaster) ? CreateCreatedResponse(new GeneralTaxGroupResponse { GeneralTaxGroupModel = taxGroupMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTaxGroupMaster/GetTaxGroupMaster")]
        [HttpGet]
        [Produces(typeof(GeneralTaxGroupResponse))]
        public IActionResult GetTaxGroupMaster(short generalTaxGroupMasterId)
        {
            try
            {
                GeneralTaxGroupModel generalTaxGroupModel = _generalTaxGroupMasterService.GetTaxGroupMaster(generalTaxGroupMasterId);
                return IsNotNull(generalTaxGroupModel) ? CreateOKResponse(new GeneralTaxGroupResponse() { GeneralTaxGroupModel = generalTaxGroupModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralTaxGroupMaster/UpdateTaxGroupMaster")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralTaxGroupResponse))]
        public IActionResult UpdateTaxGroupMaster([FromBody] GeneralTaxGroupModel model)
        {
            try
            {
                bool isUpdated = _generalTaxGroupMasterService.UpdateTaxGroupMaster(model);
                return isUpdated ? CreateOKResponse(new GeneralTaxGroupResponse { GeneralTaxGroupModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TaxGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTaxGroupResponse { HasError = true, ErrorMessage = ex.Message });
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