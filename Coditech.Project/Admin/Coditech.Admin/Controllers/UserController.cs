using Coditech.Admin.Agents;
using Coditech.Admin.ViewModel;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class UserController : BaseController
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
        public IActionResult Login(UserLoginViewModel userLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(userLoginViewModel.UserName) && !string.IsNullOrEmpty(userLoginViewModel.Password))
                {
                    userLoginViewModel = _userAgent.Login(userLoginViewModel);
                    if (!userLoginViewModel.HasError)
                    {
                        if (!string.IsNullOrEmpty(Request.Form["ReturnUrl"]))
                        {
                            return RedirectToAction(Request.Form["ReturnUrl"].ToString().Split('/')[2]);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                    }
                    ModelState.AddModelError("ErrorMessage", userLoginViewModel.ErrorMessage);
                }
            }
            else
            {
                ModelState.AddModelError("ErrorMessage", "Invalid Email Address or Password");
            }
            return View("~/Views/User/Login.cshtml", userLoginViewModel);
        }
    }
}