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
    public class GymMembershipPlanController : BaseController
    {
        private readonly IGymMembershipPlanService _generalGymMembershipPlanService;
        protected readonly ICoditechLogging _coditechLogging;
        public GymMembershipPlanController(ICoditechLogging coditechLogging, IGymMembershipPlanService generalGymMembershipPlanService)
        {
            _generalGymMembershipPlanService = generalGymMembershipPlanService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GymMembershipPlan/GetGymMembershipPlanList")]
        [Produces(typeof(GymMembershipPlanListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetGymMembershipPlanList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GymMembershipPlanListModel list = _generalGymMembershipPlanService.GetGymMembershipPlanList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GymMembershipPlanListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GymMembershipPlanListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GymMembershipPlanListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GymMembershipPlan/GetGymMemberOthershipPlan")]
        [HttpGet]
        [Produces(typeof(GymMembershipPlanResponse))]
        public virtual IActionResult GetGymMemberOthershipPlan(int gymMemberDetailId)
        {
            try
            {
                GymMembershipPlanModel gymMembershipPlanModel = _generalGymMembershipPlanService.GetGymMemberOthershipPlan(gymMemberDetailId);
                return IsNotNull(gymMembershipPlanModel) ? CreateOKResponse(new GymMembershipPlanResponse { GymMembershipPlanModel = gymMembershipPlanModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GymMembershipPlanResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GymMembershipPlanResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GymMembershipPlan/UpdateGymMemberOthershipPlan")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GymMembershipPlanResponse))]
        public virtual IActionResult UpdateGymMemberOthershipPlan([FromBody] GymMembershipPlanModel model)
        {
            try
            {
                bool isUpdated = _generalGymMembershipPlanService.UpdateGymMemberOthershipPlan(model);
                return isUpdated ? CreateOKResponse(new GymMembershipPlanResponse { GymMembershipPlanModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GymMembershipPlanResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GymMembershipPlanResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GymMembershipPlan/DeleteGymMembers")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteGymMembers([FromBody] ParameterModel gymMemberDetailIds)
        {
            try
            {
                bool deleted = _generalGymMembershipPlanService.DeleteGymMembers(gymMemberDetailIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        #region Member Follow Up
        [HttpGet]
        [Route("/GymMembershipPlan/GymMemberFollowUpList")]
        [Produces(typeof(GymMemberFollowUpListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GymMemberFollowUpList(int gymMemberDetailId, long personId, ExpandCollection expand, FilterCollection filter, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GymMemberFollowUpListModel list = _generalGymMembershipPlanService.GymMemberFollowUpList(gymMemberDetailId, personId, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GymMemberFollowUpListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GymMemberFollowUpListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GymMemberFollowUpListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        #endregion
    }
}