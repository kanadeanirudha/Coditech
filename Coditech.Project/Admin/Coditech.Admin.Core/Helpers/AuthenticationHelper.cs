using Coditech.Admin.Utilities;
using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;          
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Net;
using System.Security.Claims;
using System.Web;

namespace Coditech.Admin.Helpers
{
    public class AuthenticationHelper : IAuthorizationFilter, IAuthenticationHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string defaultControllerName = "User";
        private string defaultActionName = "UnauthorizedRequest";
        private string textReturnUrl = "returnUrl";

        public AuthenticationHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // Skip if [AllowAnonymous]
            bool isAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                .OfType<AllowAnonymousAttribute>().Any();

            if (isAllowAnonymous)
                return;

            // Not authenticated
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                HandleUnauthorizedRequest(context);
                return;
            }

            // Custom check: e.g. Admin only
            if (!SessionProxyHelper.IsAdminUser())
            {
                var controllerName = context.RouteData.Values["controller"]?.ToString()?.ToLower();
                //var actionName = context.RouteData.Values["action"]?.ToString();
                //var permissionKey = $"{controllerName}/{actionName}".ToLower();

                if (!IsAllowed(controllerName))
                {
                    HandleUnauthorizedRequest(context);
                    return;
                }
            }
        }

        private bool IsAllowed(string controllerName)
        {
            var unrestricted = new List<string>
            {
                "dashboard",
                "user",
                "generalcommon"
            };
            if (unrestricted.Contains(controllerName))
                return true;
            else
            {
                UserModel userModel = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession);
                if (userModel != null && userModel?.MenuList.Count > 0)
                    return SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession).MenuList.Where(x => !string.IsNullOrEmpty(x.ParentMenuCode)).Any(x => x.ControllerName.ToLower().Contains(controllerName.ToLower()));
                else
                    return false;
            }
        }

        private void HandleUnauthorizedRequest(AuthorizationFilterContext context)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var response = _httpContextAccessor.HttpContext.Response;

            string returnUrl = request.GetDisplayUrl();

            if (request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                var result = new
                {
                    ErrorCode = "101",
                    ReturnUrl = HttpUtility.UrlEncode(returnUrl),
                    Area = context.RouteData.Values["area"]?.ToString() ?? ""
                };
                context.Result = new JsonResult(result);
            }
            else
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "area", "" },
                    { "controller", defaultControllerName },
                    { "action", defaultActionName },
                    //{ textReturnUrl, HttpUtility.UrlEncode(returnUrl) }
                });
            }
        }
        //Set Authentication cookied for the logged in user
        public virtual async Task SetAuthCookie(string userName, bool createPersistantCookie)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.
                IsPersistent = createPersistantCookie
            };
            await HttpContextHelper.Current.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

    }
}
