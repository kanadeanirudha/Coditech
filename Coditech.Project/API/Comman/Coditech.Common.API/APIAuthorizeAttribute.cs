using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;

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

                //if (ApiSettings.EnableTokenBasedAuthorization)
                //{
                //    var headers = actionContext.HttpContext.Request.Headers;
                //    string? encodedString = headers.ContainsKey("Token") ? headers["Token"].First() : string.Empty;

                //    if (!string.IsNullOrEmpty(encodedString))
                //    {
                //        string key = EncryptionLibrary.DecryptText(encodedString);

                //        string[] parts = key.Split(new char[] { '|' });
                //        if (parts.Length > 0)
                //        {
                //            string domainName = parts[0];
                //            string webApiKey = parts[1];
                //            if (string.IsNullOrEmpty(webApiKey))
                //            {
                //                SetErrorCodes(Libraries.Common.Exceptions.ErrorCodes.WebAPIKeyNotFound, "You are unauthorized to access this resource", HttpStatusCode.Unauthorized, actionContext);
                //            }
                //            // Validate the Token expiration time.
                //            if (!_serviceProvider.GetService<IHelperUtilityService>().ValidateToken(parts[3], webApiKey, domainName))
                //            {
                //                validFlag = false;
                //                SetErrorCodes(Libraries.Common.Exceptions.ErrorCodes.UnAuthorized, "Token is expired", HttpStatusCode.Unauthorized, actionContext);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        SetErrorCodes(Libraries.Common.Exceptions.ErrorCodes.UnAuthorized, "Token is invalid", HttpStatusCode.Unauthorized, actionContext);
                //    }
                //}
                //else if (CoditechApiSettings.EnableBasicAuthorization)

                //else if (CoditechApiSettings.EnableBasicAuthorization)
                //{
                //    string? authValue = actionContext.HttpContext.Request.Headers["Authorization"].First();
                //    string[] authHeader = CoditechTokenHelper.GetAuthHeader(authValue);

                //    if (!Equals(authHeader, null))
                //    {
                //        validFlag = _serviceProvider.GetService<IHelperUtilityService>().CheckAuthHeader(authHeader[0], authHeader[1]);
                //    }
                //}
                return validFlag;
            }
            catch (Exception ex)
            {
                return false;
            }
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