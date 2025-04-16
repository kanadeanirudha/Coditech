using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Logger;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Controllers
{
    public class AccGLOpeningBalanceController : BaseController
    {
        private readonly IAccGLOpeningBalanceService _accGLOpeningBalanceService;
        protected readonly ICoditechLogging _coditechLogging;
        public AccGLOpeningBalanceController(ICoditechLogging coditechLogging, IAccGLOpeningBalanceService accGLOpeningBalanceService)
        {
            _accGLOpeningBalanceService = accGLOpeningBalanceService;
            _coditechLogging = coditechLogging;
        }

        [Route("/AccGLOpeningBalance/GetNonControlHeadType")]
        [HttpGet]
        [Produces(typeof(ACCGLOpeningBalanceListResponse))]
        public virtual IActionResult GetNonControlHeadType(int accSetupBalanceSheetId, short accSetupCategoryId, byte controlNonControl)
        {
            try
            {
                ACCGLOpeningBalanceListModel list = _accGLOpeningBalanceService.GetNonControlHeadType(accSetupBalanceSheetId, accSetupCategoryId, controlNonControl);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<ACCGLOpeningBalanceListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLOpeningBalance.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new ACCGLOpeningBalanceListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLOpeningBalance.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new ACCGLOpeningBalanceListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccGLOpeningBalance/UpdateNonControlHeadType")]
        [HttpPut, ValidateModel]
        [Produces(typeof(ACCGLOpeningBalanceResponse))]
        public virtual IActionResult UpdateNonControlHeadType([FromBody] ACCGLOpeningBalanceModel model)
        {
            try
            {
                ACCGLOpeningBalanceModel accGLOpeningBalanceModel = _accGLOpeningBalanceService.UpdateNonControlHeadType(model);
                return IsNotNull(accGLOpeningBalanceModel) ? CreateCreatedResponse(new ACCGLOpeningBalanceResponse { ACCGLOpeningBalanceModel = accGLOpeningBalanceModel }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLOpeningBalance.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new ACCGLOpeningBalanceResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLOpeningBalance.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new ACCGLOpeningBalanceResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
