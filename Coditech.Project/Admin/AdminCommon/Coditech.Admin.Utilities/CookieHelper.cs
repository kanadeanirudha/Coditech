using Coditech.Common.Helper;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Http;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Coditech.Admin.Utilities
{
    public static class CookieHelper
    {
        /// <summary>
        /// Gets the cookie value from the request or response.
        /// </summary>
        /// <param name="name">The name of the cookie.</param>
        /// <returns>The value of the cookie.</returns>
        public static string GetCookie(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                string? value;
                //Read cookie from Response. Cookie will be available in Response if server round trip has not happend after adding cookie.           
                if (IsExistsResponseCookie(name, out value))
                    return value;

                // Read cookie from Request. Cookie will be available in Request after server round trip.
                if (HttpContextHelper.Request.Cookies.ContainsKey(name))
                {
                    HttpContextHelper.Current?.Request?.Cookies?.TryGetValue(name, out value);
                    return value;
                }
            }
            return null;
        }

        /// <summary>
        /// Checks if a response cookie exists and returns its value.
        /// </summary>
        /// <param name="cookieName">The name of the cookie.</param>
        /// <param name="value">The value of the cookie.</param>
        /// <returns>True if the cookie exists, false otherwise.</returns>
        private static bool IsExistsResponseCookie(string cookieName, out string? value)
        {
            var cookieSetHeader = HttpContextHelper.Current?.Response?.GetTypedHeaders()?.SetCookie;
            var cookie = cookieSetHeader?.FirstOrDefault(x => x.Name == cookieName && !string.IsNullOrEmpty(x.Value.ToString()));
            value = cookie?.Value.ToString();

            if (!string.IsNullOrEmpty(value))
                value = Uri.UnescapeDataString(value);

            return cookieSetHeader?.Any(x => x.Name == cookieName && !string.IsNullOrEmpty(x.Value.ToString())) ?? false;
        }

        /// <summary>
        /// To set or update HttpCookies with httponly flag as default true.
        /// This method should be use when httponly cookies needs to be created so that those  
        /// Cookies can be accessible from server application only and not from the client application.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="cookieExpireInMinutes">Cookie expiration time. If time is not provide then cookie will set as default time. You may use CoditechConstants for parameter</param>
        /// <param name="isCookieSecure">Set Secure property of cookie. If parameter is null then it will set default setting</param>
        /// <returns></returns>
        public static void SetHttpOnlyCookie(string name, string value, double cookieExpireInMinutes = 0, bool? isCookieSecure = null)
            => SetCookie(name, value, cookieExpireInMinutes, true, isCookieSecure);

        /// <summary>
        /// To set or update HttpCookies
        /// </summary>
        /// <param name="cookie">Object of cookie</param>
        /// <param name="cookieExpireInMinutes">Cookie expiration time. If time is not provide then cookie will set as default time</param>
        /// <param name="IsCookieHttpOnly">Set HttpOnly property of cookie. If parameter is null then it will set default setting</param>
        /// <param name="IsCookieSecure">Set Secure property of cookie. If parameter is null then it will set default setting</param>
        /// <returns></returns>
        public static void SetCookie(string name = "", string value = "", double cookieExpireInMinutes = 0, bool? isCookieHttpOnly = null, bool? isCookieSecure = null)
        {
            try
            {
                CookieOptions cookieOptions = new CookieOptions();

                if (cookieExpireInMinutes != 0)
                    cookieOptions.Expires = DateTime.Now.AddMinutes(cookieExpireInMinutes);

                cookieOptions.HttpOnly = isCookieHttpOnly ?? CoditechAdminSettings.IsCookieHttpOnly;

                cookieOptions.Secure = isCookieSecure ?? CoditechAdminSettings.IsCookieSecure;

                HttpContextHelper.Response.Cookies.Append(name, value, cookieOptions);

            }
            catch (Exception ex)
            {
                CoditechLoggingHelper.CoditechLogging.LogMessage(ex.Message, "CookieHelper", TraceLevel.Error);
            }
        }

        /// <summary>
        /// To Get value of cookie
        /// </summary>
        /// <param name="name">Name whoose value to get</param>
        /// <returns>T</returns>
        public static T GetCookieValue<T>(string name)
        {
            string value = "";

            value = GetCookieValueFromRequest(HttpContextHelper.Current?.Request, name);
            if (string.IsNullOrEmpty(value))
                HttpContextHelper.Current?.Request?.Cookies?.TryGetValue(name, out value);
            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Gets the cookie value from the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cookieName">Name of the cookie.</param>
        /// <returns>The cookie value.</returns>
        private static string GetCookieValueFromRequest(HttpRequest request, string cookieName)
        {
            //string cookieString = request?.Headers?.Cookie.ToString();
            //if (string.IsNullOrEmpty(cookieString))
            //    return null;

            //if (cookieString.StartsWith($"{cookieName}="))
            //{
            //    var index = cookieString.IndexOf('=');
            //    var value = cookieString.IndexOf(';');
            //    return cookieString.Substring(index + 1, value - index - 1);
            //}
            return null;
        }

        /// <summary>
        /// To Get values of cookie
        /// </summary>
        /// <param name="name">Name whoose value to get</param>
        /// <returns>NameValueCollection</returns>
        public static NameValueCollection GetCookieValues(string name)
        {
            string value = "";
            HttpContextHelper.Current?.Request?.Cookies?.TryGetValue(name, out value);
            NameValueCollection cookiesCollection = new NameValueCollection();
            cookiesCollection.Add(name, value);
            return cookiesCollection;
        }

        /// <summary>
        /// To Check if cookie exists 
        /// </summary>
        /// <param name="name">Name whoose value to get</param>
        /// <returns>bool</returns>
        public static bool IsCookieExists(string name)
                => HelperUtility.IsNull(GetCookie(name)) ? false : true;

        /// <summary>  
        /// To remove http cookie  
        /// </summary>  
        /// <param name="name">name</param>  
        public static void RemoveCookie(string name)
        {
            if (HelperUtility.IsNotNull(GetCookie(name)))
                HttpContextHelper.Response?.Cookies?.Delete(name);
        }
    }
}
