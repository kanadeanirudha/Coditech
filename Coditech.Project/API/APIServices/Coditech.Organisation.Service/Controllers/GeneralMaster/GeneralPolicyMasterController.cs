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
        public virtual IActionResult GetPolicy(short generalPolicyMasterId)
        {
            try
            {
                GeneralPolicyModel generalPolicyMasterModel = _generalPolicyMasterService.GetPolicy(generalPolicyMasterId);
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
    }
}