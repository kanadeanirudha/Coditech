using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Diagnostics.Contracts;
using System.Net;

namespace Coditech.Common.API
{
    public class APIAuthorizeAttribute : IAuthorizationFilter
    {
        public int ErrorCode { get; set; } = ErrorCodes.UnAuthorized;
        public string ErrorMessage { get; set; } = "You are unauthorized to access this resource";
        public HttpStatusCode Httpcode { get; set; } = HttpStatusCode.Unauthorized;

        private readonly IServiceProvider _serviceProvider;
        public APIAuthorizeAttribute(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public void OnAuthorization(AuthorizationFilterContext actionContext)
        {

            if (Authorize(actionContext))
                return;

            HandleUnauthorizedRequest(actionContext);
        }
        protected void HandleUnauthorizedRequest(AuthorizationFilterContext actionContext)
        {
            BaseResponse data = new BaseResponse();
            data.ErrorCode = ErrorCode;
            data.ErrorMessage = ErrorMessage;
            actionContext.Result = new BadRequestObjectResult(data);
        }

        protected virtual bool Authorize(AuthorizationFilterContext actionContext)
        {
            try
            {
                bool validFlag = true;
                //If Authorization not required then return true
                if (!Convert.ToBoolean(ApiSettings.ValidateRequestAPI))
                    return validFlag;

                //SkipAuthorization get sets to true when the action has the [AllowAnonymous] attribute, If true then skip authentication.
                if (SkipAuthorization(actionContext))
                    return validFlag;

                validFlag = false;
                // Safely get Authorization header value
                string authValue = actionContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                string[] authHeader = GetAuthHeader(authValue);

                if (!Equals(authHeader, null))
                {
                    // Normalize domain values so requests work regardless of http/https or trailing slashes
                    string expectedDomain = NormalizeDomain(ApiSettings.CoditechApiDomainName);
                    string providedDomain = NormalizeDomain(authHeader[0]);

                    if (expectedDomain == providedDomain && ApiSettings.CoditechApiDomainKey == authHeader[1])
                    {
                        validFlag = true;
                    }
                }
                return validFlag;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static string NormalizeDomain(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            value = value.Trim();

            // Try parse as absolute URI to extract host (and port if present)
            if (Uri.TryCreate(value, UriKind.Absolute, out var uri))
            {
                return uri.IsDefaultPort ? uri.Host.ToLowerInvariant() : ($"{uri.Host.ToLowerInvariant()}:{uri.Port}");
            }

            // If missing scheme, try adding http to parse
            if (Uri.TryCreate("http://" + value, UriKind.Absolute, out var uri2))
            {
                return uri2.IsDefaultPort ? uri2.Host.ToLowerInvariant() : ($"{uri2.Host.ToLowerInvariant()}:{uri2.Port}");
            }

            // Fallback: remove any trailing slashes and lower-case
            return value.TrimEnd('/').ToLowerInvariant();
        }
        public static string[] GetAuthHeader(string authValue)
        {
            if (string.IsNullOrEmpty(authValue))
            {
                return null;
            }

            authValue = authValue.Remove(0, 6);
            string text = HelperUtility.DecodeBase64(authValue);
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            return text.Split('|');
        }
        private static bool SkipAuthorization(AuthorizationFilterContext actionContext)
        {
            Contract.Assert(actionContext != null);
            return actionContext.ActionDescriptor.EndpointMetadata
                                 .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));
        }

        private void SetErrorCodes(int error_code, string error_message, HttpStatusCode httpstatus_code, AuthorizationFilterContext actionContext)
        {
            ErrorCode = error_code;
            ErrorMessage = error_message;
            Httpcode = httpstatus_code;
            HandleUnauthorizedRequest(actionContext);
        }
    }
}