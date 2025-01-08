using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class ErrorPageController : BaseController
    {

        public virtual ActionResult Index()
        {
            return Redirect("/Error");
        }

        public virtual ActionResult PageNotFound()
        {
            return Redirect("/404");
        }

        public virtual ActionResult UnAuthorizedErrorRequest() => View("UnAuthorizedRequest");
    }
}
