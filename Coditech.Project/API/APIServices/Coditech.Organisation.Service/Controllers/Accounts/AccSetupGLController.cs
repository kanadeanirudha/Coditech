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
    public class AccSetupGLController : BaseController
    {
        private readonly IAccSetupGLService _accSetupGLService;
        protected readonly ICoditechLogging _coditechLogging;
        public AccSetupGLController(ICoditechLogging coditechLogging, IAccSetupGLService accSetupGLService)
        {
            _accSetupGLService = accSetupGLService;
            _coditechLogging = coditechLogging;
        }

        [Route("/AccSetupGL/GetAccSetupGLTree")]
        [HttpGet]
        [Produces(typeof(AccSetupGLResponse))]
        public virtual IActionResult GetAccSetupGLTree(string selectedcentreCode, byte accSetupBalanceSheetTypeId, int accSetupBalanceSheetId)
        {
            try
            {
                AccSetupGLModel accSetupGLModel = _accSetupGLService.GetAccSetupGLTree(selectedcentreCode, accSetupBalanceSheetTypeId, accSetupBalanceSheetId);
                return IsNotNull(accSetupGLModel) ? CreateOKResponse(new AccSetupGLResponse { AccSetupGLModel = accSetupGLModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupGLResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupGLResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/AccSetupGL/CreateAccountSetupGL")]
        [HttpPost, ValidateModel]
        [Produces(typeof(AccSetupGLResponse))]
        public virtual IActionResult CreateAccountSetupGL([FromBody] AccSetupGLModel model)
        {
            try
            {
                AccSetupGLModel CreateAccountSetupGL = _accSetupGLService.CreateAccountSetupGL(model);
                return IsNotNull(CreateAccountSetupGL) ? CreateCreatedResponse(new AccSetupGLResponse { AccSetupGLModel = CreateAccountSetupGL }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupGLResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupGLResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccSetupGL/UpdateAccountSetupGL")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AccSetupGLResponse))]
        public virtual IActionResult UpdateAccountSetupGL([FromBody] AccSetupGLModel model)
        {
            try
            {
                bool isUpdated = _accSetupGLService.UpdateAccountSetupGL(model);
                return isUpdated ? CreateOKResponse(new AccSetupGLResponse { AccSetupGLModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupGLResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupGLResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccSetupGL/AddChild")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AccSetupGLResponse))]
        public virtual IActionResult AddChild([FromBody] AccSetupGLModel model)
        {
            try
            {
                bool isUpdated = _accSetupGLService.AddChild(model);
                return isUpdated ? CreateOKResponse(new AccSetupGLResponse { AccSetupGLModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupGLResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupGLResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        
        [Route("/AccSetupGL/DeleteAccountSetupGL")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteAccountSetupGL([FromBody] ParameterModel accSetupGLIds)
        {
            try
            {
                bool deleted = _accSetupGLService.DeleteAccountSetupGL(accSetupGLIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/AccSetupGL/GetAccountSetupGL")]
        [HttpGet]
        [Produces(typeof(AccSetupGLResponse))]
        public virtual IActionResult GetAccountSetupGL(int accSetupGLId)
        {
            try
            {
                AccSetupGLModel accSetupGLModel = _accSetupGLService.GetAccountSetupGL(accSetupGLId);
                return IsNotNull(accSetupGLModel) ? CreateOKResponse(new AccSetupGLResponse { AccSetupGLModel = accSetupGLModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupGLResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupGLResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
