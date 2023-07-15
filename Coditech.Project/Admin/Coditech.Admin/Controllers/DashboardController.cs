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
            return View();
        }
    }
}
