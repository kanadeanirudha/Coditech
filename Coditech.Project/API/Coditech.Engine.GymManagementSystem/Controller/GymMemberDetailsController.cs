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
        private readonly IGeneralGymMemberDetailsService _generalGymMemberDetailsService;
        protected readonly ICoditechLogging _coditechLogging;
        public GymMemberDetailsController(ICoditechLogging coditechLogging, IGeneralGymMemberDetailsService generalGymMemberDetailsService)
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
    }
}