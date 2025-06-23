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
    public class OrganisationCentrewiseAccountSetupController : BaseController
    {
        private readonly IOrganisationCentrewiseAccountSetupService _organisationCentrewiseAccountSetupService;
        protected readonly ICoditechLogging _coditechLogging;
        public OrganisationCentrewiseAccountSetupController(ICoditechLogging coditechLogging, IOrganisationCentrewiseAccountSetupService organisationCentrewiseAccountSetupService)
        {
            _organisationCentrewiseAccountSetupService = organisationCentrewiseAccountSetupService;
            _coditechLogging = coditechLogging;
        }

        [Route("/OrganisationCentrewiseAccountSetup/GetOrganisationCentrewiseAccountSetup")]
        [HttpGet]
        [Produces(typeof(OrganisationCentrewiseAccountSetupResponse))]
        public virtual IActionResult GetOrganisationCentrewiseAccountSetup(string centreCode)
        {
            try
            {
                OrganisationCentrewiseAccountSetupModel organisationCentrewiseAccountSetupModel = _organisationCentrewiseAccountSetupService.GetOrganisationCentrewiseAccountSetup(centreCode);
                return IsNotNull(organisationCentrewiseAccountSetupModel) ? CreateOKResponse(new OrganisationCentrewiseAccountSetupResponse() { OrganisationCentrewiseAccountSetupModel = organisationCentrewiseAccountSetupModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseAccountSetup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseAccountSetupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseAccountSetup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseAccountSetupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentrewiseAccountSetup/UpdateOrganisationCentrewiseAccountSetup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseAccountSetupResponse))]
        public virtual IActionResult UpdateOrganisationCentrewiseAccountSetup([FromBody] OrganisationCentrewiseAccountSetupModel model)
        {
            try
            {
                bool isUpdated = _organisationCentrewiseAccountSetupService.UpdateOrganisationCentrewiseAccountSetup(model);
                return isUpdated ? CreateOKResponse(new OrganisationCentrewiseAccountSetupResponse { OrganisationCentrewiseAccountSetupModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseAccountSetup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseAccountSetupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseAccountSetup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseAccountSetupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}