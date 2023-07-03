using Coditech.Admin.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Coditech.Admin.Helpers
{
    public static class CookieBaseAuthenticationExtension
    {
        public static void AddCookieBaseAuthentication(this WebApplicationBuilder builder)
        {
            #region Cookie Based Authentication
            var appSetting = builder.Configuration.GetSection("appsettings");

            /// <summary>
            /// Registers services required by authentication services. <paramref name="defaultScheme"/> specifies the name of the
            /// scheme to use by default when a specific scheme isn't requested.
            /// </summary>
            /// <param name="services">The <see cref="IServiceCollection"/>.</param>
            /// <param name="defaultScheme">The default scheme used as a fallback for all other schemes.</param>
            /// <returns>A <see cref="AuthenticationBuilder"/> that can be used to further configure authentication.</returns>
            /// 
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(appSetting.GetSection("AuthenticationCookieExpireTimeSpan").Value));
                options.SlidingExpiration = Convert.ToBoolean(appSetting.GetSection("AuthenticationSlidingExpiration").Value);
                options.AccessDeniedPath = AdminConstants.LogoutPath;
                options.LogoutPath = AdminConstants.LogoutPath;
                options.LoginPath = AdminConstants.LoginPath;
            });
            #endregion
        }
    }
}
