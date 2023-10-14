using Coditech.Admin.Utilities;
using Coditech.Common.Helper;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

using Newtonsoft.Json;

using System.Security.Claims;
using System.Web;

namespace Coditech.Admin.Helpers
{
    public class AuthenticationHelper : AuthorizeAttribute, IAuthenticationHelper
    {
        private string permission;
        private string actionName = string.Empty;
        private string controllerName = string.Empty;
        private string defaultControllerName = "User";
        private string defaultActionName = "Login";
        private string textReturnUrl = "returnUrl";
        public readonly IHttpContextAccessor _httpContextAccessor;

        public string PermissionKey
        {
            get
            {
                return permission;
            }
            set
            {
                permission = value;
            }
        }

        public AuthenticationHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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


        //Redirect to login view in case user is not authenticate.
        public virtual void RedirectFromLoginPage(string userName, bool createPersistantCookie)
        {

        }
        //TODO Team Hornbills 
        // {=> FormsAuthentication.RedirectFromLoginPage(userName, createPersistantCookie);

        //Overloaded method for Authorize attribute, user to authenticate & authorize the user for each action.
        public void OnAuthorization(AuthorizationHandlerContext filterContext) => AuthenticateUser(filterContext);

        //Method Used to Authenticate the user.
        public virtual void AuthenticateUser(AuthorizationHandlerContext filterContext)
        {
            var httpContext = new DefaultHttpContext();
            var routeData = new RouteData();
            var actionDescriptor = new ActionDescriptor();
            var actionContext = new ActionContext(httpContext, routeData, actionDescriptor);
            var actionExecutingContext = new ActionExecutingContext(actionContext, new List<IFilterMetadata>(), new Dictionary<string, object>(), new object());
            var isAuthorized = HttpContextHelper.Current.User.Identity.IsAuthenticated;
            var controllerActionDescriptor = actionExecutingContext.ActionDescriptor as ControllerActionDescriptor;
            var user = HttpContextHelper.Current.User;

            bool skipAuthorization = actionExecutingContext.HttpContext.GetEndpoint().Metadata.GetMetadata<AllowAnonymousAttribute>() != null
                            || controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), inherit: true).Any();
            if (!skipAuthorization)
            {
                if (!isAuthorized && !user.Identity.IsAuthenticated && (string.IsNullOrEmpty(HttpContextHelper.Current.User.Identity.Name)))
                    HandleUnauthorizedRequest(filterContext);
                else

                {
                    if (!SessionProxyHelper.IsAdminUser())
                    {
                        if (!AuthorizeRequest(filterContext))
                            HandleUnauthorizedRequest(filterContext);
                    }
                }
            }
        }

        #region HandleUnauthorizedRequest
        //Redirect User to Index page in case the un authorized access.
        protected async void HandleUnauthorizedRequest(AuthorizationHandlerContext filterContext)
        {
            string returnUrl = (AjaxHelper.IsAjaxRequest)
                 ? (!Equals(HttpContextHelper.Request.Headers["Referer"].ToString(), null))
                     ? string.Join(HttpContextHelper.Request.Path, HttpContextHelper.Request.QueryString)
                     : string.Empty
                 : (Equals(HttpContextHelper.Request.Headers, HttpMethod.Post.ToString()))
                 ? (!Equals(HttpContextHelper.Request.Headers["Referer"].ToString(), null))
                     ? string.Join(HttpContextHelper.Request.Path, HttpContextHelper.Request.QueryString)
            : string.Empty
                 : HttpContextHelper.Request.GetDisplayUrl();

            returnUrl = returnUrl.Contains(textReturnUrl) ? HttpContextHelper.Request.GetDisplayUrl() : returnUrl;

            if (AjaxHelper.IsAjaxRequest)
            {
                HttpContextHelper.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                string routeName = (Equals(HttpContextHelper.Request.RouteValues[AdminConstants.AreaKey], null)) ? string.Empty : Convert.ToString(HttpContextHelper.Request.RouteValues[AdminConstants.AreaKey]);
                routeName = (string.IsNullOrEmpty(routeName)) ? GetAreaNameFromUrlReferrer(filterContext) : routeName;

                HttpContextHelper.Response.StatusCode = Convert.ToInt32(HttpUtility.UrlEncode(returnUrl));

                await HttpContextHelper.Response.WriteAsync(JsonConvert.SerializeObject(new { ErrorCode = "101", ReturnUrl = HttpUtility.UrlEncode(returnUrl), Area = routeName }));

            }
            else
            {
                HttpContextHelper.Request.RouteValues[AdminConstants.AreaKey] = string.Empty;

                await HttpContextHelper.Response.WriteAsync(JsonConvert.SerializeObject(new RedirectToRouteResult(
                          new RouteValueDictionary {
                        { AdminConstants.AreaKey, string.Empty },
                        { AdminConstants.Controller, defaultControllerName },
                        { AdminConstants.Action, defaultActionName },
                        { textReturnUrl, HttpUtility.UrlEncode(returnUrl)}
                        })));
            }
        }

        #endregion

        #region CreateKey
        // Retrieves the Current Action & Controller Name.       
        protected virtual void CreateKey(AuthorizationHandlerContext filterContext)
        {
            controllerName = HttpContextHelper.Request.RouteValues[AdminConstants.Controller].ToString();
            actionName = HttpContextHelper.Request.RouteValues[AdminConstants.Action].ToString();
            PermissionKey = $"{controllerName}/{actionName}";
        }
        #endregion

        // Get Area name from the current request UrlReferrer
        protected virtual string GetAreaNameFromUrlReferrer(AuthorizationHandlerContext filterContext)
        {
            string areaName = string.Empty;
            var fullUrl = HttpContextHelper.Request.Headers["Referer"].ToString();
            var questionMarkIndex = fullUrl.IndexOf('?');
            string queryString = null;
            string url = fullUrl;
            if (!Equals(questionMarkIndex, -1)) // There is a QueryString
            {
                url = fullUrl.Substring(0, questionMarkIndex);
                queryString = fullUrl.Substring(questionMarkIndex + 1);
            }
            // Arranges
            var request = _httpContextAccessor.HttpContext.Request.QueryString.Add(new QueryString(queryString));
            var response = _httpContextAccessor.HttpContext.Response;
            var httpContext = _httpContextAccessor.HttpContext;//new HttpContext(request, response);

            var routeData = httpContext.GetEndpoint();
            return areaName = (Equals(HttpContextHelper.Request.RouteValues[AdminConstants.AreaKey], null)) ? string.Empty : Convert.ToString(HttpContextHelper.Request.RouteValues[AdminConstants.AreaKey]);

        }

        protected virtual bool AuthorizeRequest(AuthorizationHandlerContext filterContext)
        {
            bool result = false;
            CreateKey(filterContext);
            if (UnrestrictedPermissionKeys().Contains(PermissionKey.ToLower()))
            {
                return true;
            }
            //List<RolePermissionViewModel> lstPermissions = SessionProxyHelper.GetUserPermission();
            //result = Equals(lstPermissions, null) ? false : lstPermissions.FindIndex(w => string.Equals(PermissionKey, w.RequestUrlTemplate, StringComparison.InvariantCultureIgnoreCase)) != -1;
            return result;
        }

        //List of UnRestricted Permission Keys.
        protected virtual List<string> UnrestrictedPermissionKeys()
        {
            List<string> lstPermission = new List<string>();
            lstPermission.Add("dashboard/dashboard");
            lstPermission.Add("user/changepassword");
            return lstPermission;
        }

    }
}

