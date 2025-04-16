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
    public class GeneralFinancialYearMasterController : BaseController
    {
        private readonly IGeneralFinancialYearMasterService _generalFinancialYearMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralFinancialYearMasterController(ICoditechLogging coditechLogging, IGeneralFinancialYearMasterService generalFinancialYearMasterService)
        {
            _generalFinancialYearMasterService = generalFinancialYearMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralFinancialYearMaster/GetFinancialYearList")]
        [Produces(typeof(GeneralFinancialYearListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetFinancialYearList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralFinancialYearListModel list = _generalFinancialYearMasterService.GetFinancialYearList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralFinancialYearListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralFinancialYearListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralFinancialYearListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralFinancialYearMaster/CreateFinancialYear")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralFinancialYearResponse))]
        public virtual IActionResult CreateFinancialYear([FromBody] GeneralFinancialYearModel model)
        {
            try
            {
                GeneralFinancialYearModel FinancialYearMaster = _generalFinancialYearMasterService.CreateFinancialYear(model);
                return IsNotNull(FinancialYearMaster) ? CreateCreatedResponse(new GeneralFinancialYearResponse { GeneralFinancialYearModel = FinancialYearMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralFinancialYearResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralFinancialYearResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralFinancialYearMaster/GetFinancialYear")]
        [HttpGet]
        [Produces(typeof(GeneralFinancialYearResponse))]
        public virtual IActionResult GetFinancialYear(short generalFinancialYearMasterId)
        {
            try
            {
                GeneralFinancialYearModel generalFinancialYearMasterModel = _generalFinancialYearMasterService.GetFinancialYear(generalFinancialYearMasterId);
                return IsNotNull(generalFinancialYearMasterModel) ? CreateOKResponse(new GeneralFinancialYearResponse { GeneralFinancialYearModel = generalFinancialYearMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralFinancialYearResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralFinancialYearResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralFinancialYearMaster/UpdateFinancialYear")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralFinancialYearResponse))]
        public virtual IActionResult UpdateFinancialYear([FromBody] GeneralFinancialYearModel model)
        {
            try
            {
                bool isUpdated = _generalFinancialYearMasterService.UpdateFinancialYear(model);
                return isUpdated ? CreateOKResponse(new GeneralFinancialYearResponse { GeneralFinancialYearModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralFinancialYearResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralFinancialYearResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralFinancialYearMaster/DeleteFinancialYear")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteFinancialYear([FromBody] ParameterModel FinancialYearIds)
        {
            try
            {
                bool deleted = _generalFinancialYearMasterService.DeleteFinancialYear(FinancialYearIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralFinancialYearMaster/GetCurrentFinancialYear")]
        [HttpGet]
        [Produces(typeof(GeneralFinancialYearResponse))]
        public virtual IActionResult GetCurrentFinancialYear(int accSetupBalanceSheetId)
        {
            try
            {
                GeneralFinancialYearModel generalFinancialYearModel = _generalFinancialYearMasterService.GetCurrentFinancialYear(accSetupBalanceSheetId);
                return IsNotNull(generalFinancialYearModel) ? CreateOKResponse(new GeneralFinancialYearResponse { GeneralFinancialYearModel = generalFinancialYearModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralFinancialYearResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.FinancialYearMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralFinancialYearResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}