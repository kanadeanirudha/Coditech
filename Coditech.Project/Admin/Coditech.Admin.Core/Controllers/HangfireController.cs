using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Coditech.Admin.Controllers
{
    public class HangfireController : BaseController
    {
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View("~/Views/Hangfire/HangfireDashboard.cshtml");
        }
    }
}
