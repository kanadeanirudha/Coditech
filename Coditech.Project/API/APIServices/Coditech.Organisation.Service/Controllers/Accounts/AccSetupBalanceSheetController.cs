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
    public class AccSetupBalanceSheetController : BaseController
    {
        private readonly IAccSetupBalanceSheetService _accSetupBalanceSheetService;
        protected readonly ICoditechLogging _coditechLogging;
        public AccSetupBalanceSheetController(ICoditechLogging coditechLogging, IAccSetupBalanceSheetService accSetupBalanceSheetService)
        {
            _accSetupBalanceSheetService = accSetupBalanceSheetService;
            _coditechLogging = coditechLogging;
        }

      
        [HttpGet]
        [Route("/AccSetupBalanceSheet/GetBalanceSheetList")]
        [Produces(typeof(AccSetupBalanceSheetListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetBalanceSheetList(string selectedCentreCode, byte accSetupBalanceSheetTypeId, ExpandCollection expand, FilterCollection filter, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                AccSetupBalanceSheetListModel list = _accSetupBalanceSheetService.GetBalanceSheetList(selectedCentreCode, accSetupBalanceSheetTypeId,  filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AccSetupBalanceSheetListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupBalanceSheetListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupBalanceSheetListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
       
        [Route("/AccSetupBalanceSheet/CreateBalanceSheet")]
        [HttpPost, ValidateModel]
        [Produces(typeof(AccSetupBalanceSheetResponse))]
        public virtual IActionResult CreateBalanceSheet([FromBody] AccSetupBalanceSheetModel model)
        {
            try
            {
                AccSetupBalanceSheetModel designation = _accSetupBalanceSheetService.CreateBalanceSheet(model);
                return IsNotNull(designation) ? CreateCreatedResponse(new AccSetupBalanceSheetResponse { AccSetupBalanceSheetModel = designation }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupBalanceSheetResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupBalanceSheetResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccSetupBalanceSheet/GetBalanceSheet")]
        [HttpGet]
        [Produces(typeof(AccSetupBalanceSheetResponse))]
        public virtual IActionResult BalanceSheet(int accSetupBalanceSheetId)
        {
            try
            {
                AccSetupBalanceSheetModel accSetupBalanceSheetModel = _accSetupBalanceSheetService.GetBalanceSheet(accSetupBalanceSheetId);
                return IsNotNull(accSetupBalanceSheetModel) ? CreateOKResponse(new AccSetupBalanceSheetResponse { AccSetupBalanceSheetModel = accSetupBalanceSheetModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupBalanceSheetResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupBalanceSheetResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccSetupBalanceSheet/UpdateBalanceSheet")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AccSetupBalanceSheetResponse))]
        public virtual IActionResult UpdateBalanceSheet([FromBody] AccSetupBalanceSheetModel model)
        {
            try
            {
                bool isUpdated = _accSetupBalanceSheetService.UpdateBalanceSheet(model);
                return isUpdated ? CreateOKResponse(new AccSetupBalanceSheetResponse { AccSetupBalanceSheetModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupBalanceSheetResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupBalanceSheetResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccSetupBalanceSheet/DeleteBalanceSheet")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteBalanceSheet([FromBody] ParameterModel balanceSheetIds)
        {
            try
            {
                bool deleted = _accSetupBalanceSheetService.DeleteBalanceSheet(balanceSheetIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupBalanceSheet.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
