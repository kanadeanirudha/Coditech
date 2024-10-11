using Coditech.Admin.Utilities;

namespace Coditech.Admin
{
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// Custom middleware to handle the operation before request and after the response.
        /// </summary>
        /// <param name="next"></param>
        public MaintenanceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Configured middleware to handle or perform the operation before request execute or response return.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            bool isMaintenance = CoditechAdminSettings.MaintenanceMode;
            if (isMaintenance)
            {
                context.Response.Redirect("/maintenance.html");
                return;
            }
            await _next(context);
        }

    }
}
