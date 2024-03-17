using Coditech.Common.Exceptions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        public DashboardController()
        {
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
    }
}
