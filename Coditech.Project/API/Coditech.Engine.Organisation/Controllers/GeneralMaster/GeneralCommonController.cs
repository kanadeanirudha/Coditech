using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Controllers
{
    [ApiController]
    public class GeneralCommonController : BaseController
    {

        private readonly IGeneralCommonService _generalCommonService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralCommonController(ICoditechLogging coditechLogging, IGeneralCommonService generalCommonService)
        {
            _generalCommonService = generalCommonService;
            _coditechLogging = coditechLogging;
        }
        
        [HttpGet]
        [Route("/GeneralCommon/GetDropdownListByCode")]
        [Produces(typeof(GeneralEnumaratorListResponse))]
        public virtual IActionResult GetDropdownListByCode(string groupCodes)
        {
            try
            {
                List<GeneralEnumaratorModel> list = _generalCommonService.GetDropdownListByCode(groupCodes);
                return IsNotNull(list) ? CreateOKResponse(new GeneralEnumaratorListResponse { GeneralEnumaratorList = list }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

    }
}