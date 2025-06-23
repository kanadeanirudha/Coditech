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
    public class OrganisationCentrewisePolicyController : BaseController
    {
        private readonly IOrganisationCentrewisePolicyService _organisationCentrewisePolicyService;
        protected readonly ICoditechLogging _coditechLogging;
        public OrganisationCentrewisePolicyController(ICoditechLogging coditechLogging, IOrganisationCentrewisePolicyService organisationCentrewisePolicyService)
        {
            _organisationCentrewisePolicyService = organisationCentrewisePolicyService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/OrganisationCentrewisePolicy/GetOrganisationCentrewisePolicyList")]
        [Produces(typeof(GeneralPolicyDetailsListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetOrganisationCentrewisePolicyList(string centreCode, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralPolicyDetailsListModel list = _organisationCentrewisePolicyService.GetOrganisationCentrewisePolicyList(centreCode, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralPolicyDetailsListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewisePolicy.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyDetailsListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewisePolicy.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyDetailsListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentrewisePolicy/GetCentrewisePolicyDetails")]
        [HttpGet]
        [Produces(typeof(GeneralPolicyDetailsResponse))]
        public virtual IActionResult GetCentrewisePolicyDetails(string centreCode, short generalPolicyRulesId)
        {
            try
            {
                GeneralPolicyDetailsModel generalPolicyDetailsModel = _organisationCentrewisePolicyService.GetCentrewisePolicyDetails(centreCode, generalPolicyRulesId);
                return IsNotNull(generalPolicyDetailsModel) ? CreateOKResponse(new GeneralPolicyDetailsResponse { GeneralPolicyDetailsModel = generalPolicyDetailsModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewisePolicy.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPolicyRulesResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewisePolicy.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyRulesResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentrewisePolicy/CentrewisePolicyDetails")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralPolicyDetailsResponse))]
        public virtual IActionResult CentrewiseDetailsPolicyDetails([FromBody] GeneralPolicyDetailsModel model)
        {
            try
            {
                bool isUpdated = _organisationCentrewisePolicyService.CentrewisePolicyDetails(model);
                return isUpdated ? CreateOKResponse(new GeneralPolicyDetailsResponse { GeneralPolicyDetailsModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewisePolicy.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPolicyDetailsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewisePolicy.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyDetailsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/OrganisationCentrewisePolicy/DeleteCentrewisePolicy")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteCentrewisePolicy([FromBody] ParameterModel PolicyIds)
        {
            try
            {
                bool deleted = _organisationCentrewisePolicyService.DeleteCentrewisePolicy(PolicyIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
