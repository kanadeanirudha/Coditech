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
        private readonly IGymDashboardAgent _gymDashboardAgent;
        private readonly IDBTMDashboardAgent _dBTMDashboardAgent;
        public DashboardController(IDashboardAgent dashboardAgent, IGymDashboardAgent gymDashboardAgent, IDBTMDashboardAgent dBTMDashboardAgent)
        {
            _dashboardAgent = dashboardAgent;
            _gymDashboardAgent = gymDashboardAgent;
            _dBTMDashboardAgent = dBTMDashboardAgent;
        }

        [HttpGet]
        public virtual IActionResult Index(short numberOfDaysRecord)
        {
            DashboardViewModel dashboardViewModel = _dashboardAgent.GetDashboardDetails(numberOfDaysRecord);
            if (IsNotNull(dashboardViewModel) && !string.IsNullOrEmpty(dashboardViewModel.DashboardFormEnumCode))
            {
                if (dashboardViewModel.DashboardFormEnumCode.Equals(DashboardFormEnum.GymOwnerDashboard.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    GymDashboardViewModel gymDashboardViewModel = _gymDashboardAgent.GetGymDashboardDetails();
                    return View("~/Views/Gym/GymDashboard/GymOwnerDashboard.cshtml", gymDashboardViewModel);                  
                }
                else if (dashboardViewModel.DashboardFormEnumCode.Equals(DashboardFormEnum.GymOperatorDashboard.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    GymDashboardViewModel gymDashboardViewModel = _gymDashboardAgent.GetGymDashboardDetails();
                    return View("~/Views/Gym/GymDashboard/GymOperatorDashboard.cshtml", gymDashboardViewModel);
                }
                else if (dashboardViewModel.DashboardFormEnumCode.Equals(DashboardFormEnum.DBTMCentreDashboard.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    DBTMDashboardViewModel dBTMDashboardViewModel = _dBTMDashboardAgent.GetDBTMDashboardDetails();
                    return View("~/Views/DBTM/DBTMDashboard/DBTMCentreDashboard.cshtml", dBTMDashboardViewModel);
                }
                else if (dashboardViewModel.DashboardFormEnumCode.Equals(DashboardFormEnum.DBTMTrainerDashboard.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    DBTMDashboardViewModel dBTMDashboardViewModel = _dBTMDashboardAgent.GetDBTMDashboardDetails();
                    return View("~/Views/DBTM/DBTMDashboard/DBTMTrainerDashboard.cshtml", dBTMDashboardViewModel);
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
