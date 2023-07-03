using Coditech.Common.Helper;

using Microsoft.AspNetCore.Http;

namespace Coditech.Admin.Utilities
{
    public static class AjaxHelper
    {
        /// <summary>
        /// Checks if the current request is an AJAX request.
        /// </summary>
        public static bool IsAjaxRequest => CoditechDependencyResolver.GetService<IHttpContextAccessor>().HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
    }
}
