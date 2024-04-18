using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Controllers
{

    public class DashboardController : BaseController
    {

        private readonly IDashboardService _dashboardService;
        protected readonly ICoditechLogging _coditechLogging;
        public DashboardController(ICoditechLogging coditechLogging, IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
            _coditechLogging = coditechLogging;
        }

        [Route("/DashboardController/GetDashboard")]
        [HttpGet]
        [Produces(typeof(DashboardResponse))]
        public virtual IActionResult GetDashboard(int selectedAdminRoleMasterId)
        {
            try
            {
                DashboardModel dashboardModel = _dashboardService.GetDashboard(selectedAdminRoleMasterId);
                return IsNotNull(dashboardModel) ? CreateOKResponse(new DashboardResponse { DashboardModel = dashboardModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Dashboard.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new DashboardResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Dashboard.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new DashboardResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}