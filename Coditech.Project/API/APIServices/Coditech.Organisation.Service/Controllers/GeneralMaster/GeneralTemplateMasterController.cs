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
    public class GeneralTemplateMasterController : BaseController
    {
        private readonly IGeneralTemplateService _generalTemplateService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralTemplateMasterController(ICoditechLogging coditechLogging, IGeneralTemplateService generalTemplateService)
        {
            _generalTemplateService = generalTemplateService;
            _coditechLogging = coditechLogging;
        }

        [Route("/GeneralTemplate/GetTemplate")]
        [HttpGet]
        [Produces(typeof(GeneralTemplateResponse))]
        public virtual IActionResult GetTemplate(int generalTemplateId)
        {
            try
            {
                GeneralTemplateModel generalTemplateModel = _generalTemplateService.GetTemplate(generalTemplateId);
                return IsNotNull(generalTemplateModel) ? CreateOKResponse(new GeneralTemplateResponse { GeneralTemplateModel = generalTemplateModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, "Template", TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralTemplateResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, "Template", TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralTemplateResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
