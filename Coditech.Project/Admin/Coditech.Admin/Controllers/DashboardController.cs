using Coditech.Admin.Agents;
using Coditech.Admin.ViewModel;
using Coditech.Common.Exceptions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public virtual IActionResult Index()
        {
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

        [HttpGet]
        public virtual ActionResult Dashboard(int selectedAdminRoleMasterId)
        {
            DashboardViewModel generalCountryViewModel = _dashboardAgent.GetDashboardDetails(selectedAdminRoleMasterId);
            return View(generalCountryViewModel);
        }
    }
}
