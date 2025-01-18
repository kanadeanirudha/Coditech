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
    public class OrganisationCentrewiseJoiningCodeController : BaseController
    {
        private readonly IOrganisationCentrewiseJoiningCodeService _organisationCentrewiseJoiningCodeService;
        protected readonly ICoditechLogging _coditechLogging;

        public OrganisationCentrewiseJoiningCodeController(ICoditechLogging coditechLogging, IOrganisationCentrewiseJoiningCodeService organisationCentrewiseJoiningCodeService)
        {
            _organisationCentrewiseJoiningCodeService = organisationCentrewiseJoiningCodeService;
            _coditechLogging = coditechLogging;
        }
        [HttpGet]
        [Route("/OrganisationCentrewiseJoiningCode/GetOrganisationCentrewiseJoiningCodeList")]
        [Produces(typeof(OrganisationCentrewiseJoiningCodeListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetOrganisationCentrewiseJoiningCodeList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                OrganisationCentrewiseJoiningCodeListModel list = _organisationCentrewiseJoiningCodeService.GetOrganisationCentrewiseJoiningCodeList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<OrganisationCentrewiseJoiningCodeListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseJoiningCode.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseJoiningCodeListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseJoiningCode.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseJoiningCodeListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/OrganisationCentrewiseJoiningCode/CreateOrganisationCentrewiseJoiningCode")]
        [HttpPost, ValidateModel]
        [Produces(typeof(OrganisationCentrewiseJoiningCodeResponse))]
        public virtual IActionResult CreateOrganisationCentrewiseJoiningCode([FromBody] OrganisationCentrewiseJoiningCodeModel model)
        {
            try
            {
                OrganisationCentrewiseJoiningCodeModel organisationCentrewiseJoiningCode = _organisationCentrewiseJoiningCodeService.CreateOrganisationCentrewiseJoiningCode(model);
                return IsNotNull(organisationCentrewiseJoiningCode) ? CreateCreatedResponse(new OrganisationCentrewiseJoiningCodeResponse { OrganisationCentrewiseJoiningCodeModel = organisationCentrewiseJoiningCode }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseJoiningCode.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseJoiningCodeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseJoiningCode.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new OrganisationCentrewiseJoiningCodeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

    }
}
