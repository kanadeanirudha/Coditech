using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace Coditech.API.Controllers
{
    [ApiController]
    public class OrganisationMasterController : BaseController
    {
        private readonly IOrganisationService _organisationService;
        protected readonly ICoditechLogging _coditechLogging;
        public OrganisationMasterController(ICoditechLogging coditechLogging, IOrganisationService organisationService)
        {
            _organisationService = organisationService;
            _coditechLogging = coditechLogging;
        }
        /// <summary>
        /// Login to application.
        /// </summary>
        /// <param name="model">Organisation Model.</param>
        /// <returns>OrganisationModel</returns>
        /// 

        [Route("/OrganisationMaster/GetOrganisation")]
        [HttpGet]
        [Produces(typeof(OrganisationResponse))]
        public IActionResult GetOrganisation(short organisationMasterId)
        {
            try
            {
                OrganisationModel organisationModel = _organisationService.GetOrganisation(organisationMasterId);
                return HelperUtility.IsNotNull(organisationModel) ? CreateOKResponse(new OrganisationResponse { OrganisationModel = organisationModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Organisation.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Organisation.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationMaster/UpdateOrganisation")]
        [HttpPut, ValidateModel]
        [Produces(typeof(OrganisationResponse))]
        public IActionResult UpdateOrganisation([FromBody] OrganisationModel model)
        {
            try
            {
                OrganisationModel Organisation = _organisationService.UpdateOrganisation(model);
                return HelperUtility.IsNotNull(Organisation) ? CreateOKResponse(Organisation) : null;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Organisation.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Organisation.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}