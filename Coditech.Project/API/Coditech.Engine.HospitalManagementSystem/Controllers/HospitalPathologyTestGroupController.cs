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

namespace Coditech.Engine.HospitalManagementSystem.Controllers
{
    public class HospitalPathologyTestGroupController : BaseController
    {
        private readonly IHospitalPathologyTestGroupService _hospitalPathologyTestGroupService;
        protected readonly ICoditechLogging _coditechLogging;
        public HospitalPathologyTestGroupController(ICoditechLogging coditechLogging, IHospitalPathologyTestGroupService hospitalPathologyTestGroupService)
        {
            _hospitalPathologyTestGroupService = hospitalPathologyTestGroupService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/HospitalPathologyTestGroup/GetHospitalPathologyTestGroupList")]
        [Produces(typeof(HospitalPathologyTestGroupListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetHospitalPathologyTestGroupList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                HospitalPathologyTestGroupListModel list = _hospitalPathologyTestGroupService.GetHospitalPathologyTestGroupList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<HospitalPathologyTestGroupListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestGroupListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestGroupListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPathologyTestGroup/CreateHospitalPathologyTestGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(HospitalPathologyTestGroupResponse))]
        public virtual IActionResult CreateHospitalPathologyTestGroup([FromBody] HospitalPathologyTestGroupModel model)
        {
            try
            {
                HospitalPathologyTestGroupModel hospitalPathologyTestGroup = _hospitalPathologyTestGroupService.CreateHospitalPathologyTestGroup(model);
                return IsNotNull(hospitalPathologyTestGroup) ? CreateCreatedResponse(new HospitalPathologyTestGroupResponse { HospitalPathologyTestGroupModel = hospitalPathologyTestGroup }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPathologyTestGroup/GetHospitalPathologyTestGroup")]
        [HttpGet]
        [Produces(typeof(HospitalPathologyTestGroupResponse))]
        public virtual IActionResult GetHospitalPathologyTestGroup(int hospitalPathologyTestGroupId)
        {
            try
            {
                HospitalPathologyTestGroupModel hospitalPathologyTestGroupModel = _hospitalPathologyTestGroupService.GetHospitalPathologyTestGroup(hospitalPathologyTestGroupId);
                return IsNotNull(hospitalPathologyTestGroupModel) ? CreateOKResponse(new HospitalPathologyTestGroupResponse { HospitalPathologyTestGroupModel = hospitalPathologyTestGroupModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPathologyTestGroup/UpdateHospitalPathologyTestGroup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(HospitalPathologyTestGroupResponse))]
        public virtual IActionResult UpdateHospitalPathologyTestGroup([FromBody] HospitalPathologyTestGroupModel model)
        {
            try
            {
                bool isUpdated = _hospitalPathologyTestGroupService.UpdateHospitalPathologyTestGroup(model);
                return isUpdated ? CreateOKResponse(new HospitalPathologyTestGroupResponse { HospitalPathologyTestGroupModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalPathologyTestGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalPathologyTestGroup/DeleteHospitalPathologyTestGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteHospitalPathologyTestGroup([FromBody] ParameterModel hospitalPathologyTestGroupIds)
        {
            try
            {
                bool deleted = _hospitalPathologyTestGroupService.DeleteHospitalPathologyTestGroup(hospitalPathologyTestGroupIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}