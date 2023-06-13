using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Coditech.Common.Helper
{
    public static class HttpContextHelper
    {
        //Gets the current request.
        public static HttpRequest Request => CoditechDependencyResolver._staticServiceProvider?.GetService<IHttpContextAccessor>()?.HttpContext?.Request;
        public static HttpContext Current => CoditechDependencyResolver._staticServiceProvider?.GetService<IHttpContextAccessor>()?.HttpContext;
        public static HttpResponse Response => CoditechDependencyResolver._staticServiceProvider?.GetService<IHttpContextAccessor>()?.HttpContext.Response;

    }
}
