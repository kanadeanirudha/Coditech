using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        public virtual IActionResult Index()
        {
            return View();
        }
    }
}
