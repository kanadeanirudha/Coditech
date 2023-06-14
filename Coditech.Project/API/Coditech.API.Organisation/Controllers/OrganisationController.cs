using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace Coditech.API.Controllers
{
    [ApiController]
    public class OrganisationController : BaseController
    {
        private readonly IOrganisationService _OrganisationService;
        protected readonly ICoditechLogging _coditechLogging;
        public OrganisationController(ICoditechLogging coditechLogging, IOrganisationService OrganisationService)
        {
            _OrganisationService = OrganisationService;
            _coditechLogging = coditechLogging;
        }
        /// <summary>
        /// Login to application.
        /// </summary>
        /// <param name="model">Organisation Model.</param>
        /// <returns>OrganisationModel</returns>
        [Route("/Organisation/Login")]
        [HttpPost]
        [Produces(typeof(OrganisationModel))]
        public IActionResult UpdateOrganisation([FromBody] OrganisationModel model)
        {
            try
            {
                OrganisationModel Organisation = _OrganisationService.UpdateOrganisation(model);
                return HelperUtility.IsNotNull(Organisation) ? CreateOKResponse(Organisation) : null;
            }
            catch (CoditechUnauthorizedException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Organisation.ToString(), TraceLevel.Warning);
                return CreateUnauthorizedResponse(new OrganisationModel { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Organisation.ToString(), TraceLevel.Warning);
                return CreateUnauthorizedResponse(new OrganisationModel { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Organisation.ToString(), TraceLevel.Error);
                return CreateUnauthorizedResponse(new OrganisationModel { HasError = true, ErrorMessage = ex.Message });
            }

        }

    }
}