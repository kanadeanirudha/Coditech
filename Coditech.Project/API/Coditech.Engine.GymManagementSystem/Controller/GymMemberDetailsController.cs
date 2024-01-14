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
    public class GymMemberDetailsController : BaseController
    {
        private readonly IGymMemberDetailsService _generalGymMemberDetailsService;
        protected readonly ICoditechLogging _coditechLogging;
        public GymMemberDetailsController(ICoditechLogging coditechLogging, IGymMemberDetailsService generalGymMemberDetailsService)
        {
            _generalGymMemberDetailsService = generalGymMemberDetailsService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GymMemberDetails/GetGymMemberDetailsList")]
        [Produces(typeof(GymMemberDetailsListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetGymMemberDetailsList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GymMemberDetailsListModel list = _generalGymMemberDetailsService.GetGymMemberDetailsList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GymMemberDetailsListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GymMemberDetailsListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GymMemberDetailsListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GymMemberDetails/GetGymMemberOtherDetails")]
        [HttpGet]
        [Produces(typeof(GymMemberDetailsResponse))]
        public virtual IActionResult GetGymMemberOtherDetails(int gymMemberDetailId)
        {
            try
            {
                GymMemberDetailsModel gymMemberDetailsModel = _generalGymMemberDetailsService.GetGymMemberOtherDetails(gymMemberDetailId);
                return IsNotNull(gymMemberDetailsModel) ? CreateOKResponse(new GymMemberDetailsResponse { GymMemberDetailsModel = gymMemberDetailsModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GymMemberDetailsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GymMemberDetailsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GymMemberDetails/UpdateGymMemberOtherDetails")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GymMemberDetailsResponse))]
        public virtual IActionResult UpdateGymMemberOtherDetails([FromBody] GymMemberDetailsModel model)
        {
            try
            {
                bool isUpdated = _generalGymMemberDetailsService.UpdateGymMemberOtherDetails(model);
                return isUpdated ? CreateOKResponse(new GymMemberDetailsResponse { GymMemberDetailsModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GymMemberDetailsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GymMemberDetailsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GymMemberDetails/DeleteGymMembers")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteGymMembers([FromBody] ParameterModel gymMemberDetailIds)
        {
            try
            {
                bool deleted = _generalGymMemberDetailsService.DeleteGymMembers(gymMemberDetailIds);
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
        [Route("/GymMemberDetails/GymMemberFollowUpList")]
        [Produces(typeof(GymMemberFollowUpListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GymMemberFollowUpList(int gymMemberDetailId, long personId, ExpandCollection expand, FilterCollection filter, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GymMemberFollowUpListModel list = _generalGymMemberDetailsService.GymMemberFollowUpList(gymMemberDetailId, personId, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
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