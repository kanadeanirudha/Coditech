using Coditech.Admin.Agents;
using Coditech.Admin.ViewModel;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAgent _userAgent;

        public UserController(IUserAgent userAgent)
        {
            _userAgent = userAgent;
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            //if (User.Identity.IsAuthenticated)
            //    _userAgent.Logout();

            //GetLoginRememberMeCookie();
            return View(new UserLoginViewModel());
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(UserLoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                UserLoginViewModel loginviewModel = _userAgent.Login(viewModel);
            }
            return View();
        }
    }
}