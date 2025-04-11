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
    public class GeneralPolicyMasterController : BaseController
    {
        private readonly IGeneralPolicyMasterService _generalPolicyMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralPolicyMasterController(ICoditechLogging coditechLogging, IGeneralPolicyMasterService generalPolicyMasterService)
        {
            _generalPolicyMasterService = generalPolicyMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralPolicyMaster/GetPolicyList")]
        [Produces(typeof(GeneralPolicyListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetPolicyList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralPolicyListModel list = _generalPolicyMasterService.GetPolicyList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralPolicyListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPolicyMaster/CreatePolicy")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralPolicyResponse))]
        public virtual IActionResult CreatePolicy([FromBody] GeneralPolicyModel model)
        {
            try
            {
                GeneralPolicyModel policyMaster = _generalPolicyMasterService.CreatePolicy(model);
                return IsNotNull(policyMaster) ? CreateCreatedResponse(new GeneralPolicyResponse { GeneralPolicyModel = policyMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPolicyResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPolicyMaster/GetPolicy")]
        [HttpGet]
        [Produces(typeof(GeneralPolicyResponse))]
        public virtual IActionResult GetPolicy(string policyCode)
        {
            try
            {
                GeneralPolicyModel generalPolicyMasterModel = _generalPolicyMasterService.GetPolicy(policyCode);
                return IsNotNull(generalPolicyMasterModel) ? CreateOKResponse(new GeneralPolicyResponse { GeneralPolicyModel = generalPolicyMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPolicyResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPolicyMaster/UpdatePolicy")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralPolicyResponse))]
        public virtual IActionResult UpdatePolicy([FromBody] GeneralPolicyModel model)
        {
            try
            {
                bool isUpdated = _generalPolicyMasterService.UpdatePolicy(model);
                return isUpdated ? CreateOKResponse(new GeneralPolicyResponse { GeneralPolicyModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPolicyResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPolicyMaster/DeletePolicy")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeletePolicy([FromBody] ParameterModel PolicyIds)
        {
            try
            {
                bool deleted = _generalPolicyMasterService.DeletePolicy(PolicyIds);
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

        //General Policy Rules
        [HttpGet]
        [Route("/GeneralPolicyMaster/GetGeneralPolicyRulesList")]
        [Produces(typeof(GeneralPolicyRulesListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetGeneralPolicyRulesList(string policyCode, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralPolicyRulesListModel list = _generalPolicyMasterService.GetGeneralPolicyRulesList(policyCode, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralPolicyRulesListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyRulesListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyRulesListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPolicyMaster/CreatePolicyRules")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralPolicyRulesResponse))]
        public virtual IActionResult CreatePolicyRules([FromBody] GeneralPolicyRulesModel model)
        {
            try
            {
                GeneralPolicyRulesModel policyMaster = _generalPolicyMasterService.CreatePolicyRules(model);
                return IsNotNull(policyMaster) ? CreateCreatedResponse(new GeneralPolicyRulesResponse { GeneralPolicyRulesModel = policyMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPolicyRulesResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyRulesResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPolicyMaster/GetPolicyRules")]
        [HttpGet]
        [Produces(typeof(GeneralPolicyRulesResponse))]
        public virtual IActionResult GetPolicyRules(short generalPolicyRulesId, string policyApplicableStatus)
        {
            try
            {
                GeneralPolicyRulesModel generalPolicyRulesModel = _generalPolicyMasterService.GetPolicyRules(generalPolicyRulesId, policyApplicableStatus);
                return IsNotNull(generalPolicyRulesModel) ? CreateOKResponse(new GeneralPolicyRulesResponse { GeneralPolicyRulesModel = generalPolicyRulesModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPolicyRulesResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyRulesResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPolicyMaster/UpdatePolicyRules")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralPolicyRulesResponse))]
        public virtual IActionResult UpdatePolicyRules([FromBody] GeneralPolicyRulesModel model)
        {
            try
            {
                bool isUpdated = _generalPolicyMasterService.UpdatePolicyRules(model);
                return isUpdated ? CreateOKResponse(new GeneralPolicyRulesResponse { GeneralPolicyRulesModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPolicyRulesResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyRulesResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPolicyMaster/DeletePolicyRules")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeletePolicyRules([FromBody] ParameterModel PolicyIds)
        {
            try
            {
                bool deleted = _generalPolicyMasterService.DeletePolicyRules(PolicyIds);
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

        [Route("/GeneralPolicyMaster/GetPolicyDetails")]
        [HttpGet]
        [Produces(typeof(GeneralPolicyDetailsResponse))]
        public virtual IActionResult GetPolicyDetails(short generalPolicyDetailsId)
        {
            try
            {
                GeneralPolicyDetailsModel generalPolicyDetailsModel = _generalPolicyMasterService.GetPolicyDetails(generalPolicyDetailsId);
                return IsNotNull(generalPolicyDetailsModel) ? CreateOKResponse(new GeneralPolicyDetailsResponse { GeneralPolicyDetailsModel = generalPolicyDetailsModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPolicyDetailsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyDetailsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPolicyMaster/UpdatePolicyDetails")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralPolicyDetailsResponse))]
        public virtual IActionResult UpdatePolicyDetails([FromBody] GeneralPolicyDetailsModel model)
        {
            try
            {
                bool isUpdated = _generalPolicyMasterService.UpdatePolicyDetails(model);
                return isUpdated ? CreateOKResponse(new GeneralPolicyDetailsResponse { GeneralPolicyDetailsModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPolicyDetailsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPolicyDetailsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}