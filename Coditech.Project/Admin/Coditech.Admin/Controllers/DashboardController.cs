using Coditech.Admin.Agents;
using Coditech.Admin.ViewModel;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IDashboardAgent _dashboardAgent;
        public DashboardController(IDashboardAgent dashboardAgent)
        {
            _dashboardAgent = dashboardAgent;
        }

        [HttpGet]
        public virtual IActionResult Index(short numberOfDaysRecord)
        {
            DashboardViewModel dashboardViewModel = _dashboardAgent.GetDashboardDetails(numberOfDaysRecord);
            if (IsNotNull(dashboardViewModel) && !string.IsNullOrEmpty(dashboardViewModel.DashboardFormEnumCode))
            {
                if (dashboardViewModel.DashboardFormEnumCode.Equals(DashboardFormEnum.GymOwnerDashboard.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    return View("~/Views/Dashboard/Gym/GymOwnerDashboard.cshtml", dashboardViewModel);
                }
                else if (dashboardViewModel.DashboardFormEnumCode.Equals(DashboardFormEnum.GymOperatorDashboard.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    return View("~/Views/Dashboard/Gym/GymOwnerDashboard.cshtml", dashboardViewModel);
                }
            }

            return View("~/Views/Dashboard/GeneralDashboard.cshtml");
        }

        [AllowAnonymous]
        public virtual ActionResult TokenError(Exception exception)
        {
            CoditechException coditechException = exception as CoditechException;

            if (coditechException.ErrorCode == ErrorCodes.WebAPIKeyNotFound)
                ViewBag.ErrorMessage = "Web Api key not found please enter into web.config file.";

            return View("UnAuthorized");
        }


        [AllowAnonymous]
        public virtual ActionResult ConfigurationError(Exception exception)
        {
            CoditechException coditechException = exception as CoditechException;
            ViewBag.ErrorMessage = coditechException?.ErrorMessage ?? exception.Message;
            return View("UnAuthorized");
        }
    }
}
