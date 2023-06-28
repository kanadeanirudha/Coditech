
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    [AllowAnonymous]
    public class DashboardController : BaseController
    {
        public DashboardController()
        {
        }

        [HttpGet]
        public virtual IActionResult Index()
        {
            return View();
        }
    }
}
