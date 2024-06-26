﻿using Coditech.Common.Helper;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Coditech.API.Data
{
    public static class HelperMethods
    {
        private static readonly IHttpContextAccessor _httpContextAccessor = CoditechDependencyResolver._staticServiceProvider?.GetService<IHttpContextAccessor>();
        private static IConfiguration settings = CoditechDependencyResolver._staticServiceProvider?.GetService<IConfiguration>();

        /// <summary>
        /// Get Login User Id from Request Headers
        /// </summary>
        /// <returns>Login User Id</returns>
        public static long GetLoginUserId()
        {
            long userId = 0;
            var headers = HttpContextHelper.Request?.Headers;
            if (headers != null)
                long.TryParse(headers["LoginAsUserId"], out userId);

            return userId;
        }

        public static bool IsUserWantToDebugSql
        {
            get
            {
                return true;
                if (string.IsNullOrEmpty(settings.GetSection("appsettings")["EnableLinqSQLDebugging"]))
                    return false;
                else
                    return Convert.ToBoolean(settings.GetSection("appsettings")["EnableLinqSQLDebugging"]);
            }
        }
        // Get current datetime.
        public static DateTime GetEntityDateTime() => DateTime.Now;
    }
}
