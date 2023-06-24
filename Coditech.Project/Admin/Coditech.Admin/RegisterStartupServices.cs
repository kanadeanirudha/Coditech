using Coditech.Admin.Utilities;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.FileProviders;

namespace Coditech.Admin
{
    public static class RegisterStartupServices
    {
        /// <summary>
        /// Service level configuratin registered.
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterCommonServices(this WebApplicationBuilder builder)
        {
            //This method configures the MVC services for the commonly used features for pages.This
            // combines the effects of <see cref="MvcCoreServiceCollectionExtensions.AddMvcCore(IServiceCollection)"/>
            builder.Services.AddRazorPages();

            //Register Dependecncy.
            builder.RegisterDI();

            // Adds a default implementation for the Microsoft.AspNetCore.Http.IHttpContextAccessor
            // service. 
            builder.Services.AddHttpContextAccessor();

            //Register custom filters.
            builder.RegisterFilters();
            // Adds distributed sqlsessioncache and memorycache.
            builder.AddSession();

            //// Adds caching in the application.
            //builder.RegisterCaching();

            // Extensions to scan for AutoMapper classes and register the configuration, mapping, and extensions with the service collection:
            //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            // Adds MVC services to the specified <see cref="IServiceCollection" />.
            builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

            // Configures Newtonsoft.Json specific features such as input and output formatters.
            builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            // Adds services for controllers to the specified <see cref="IServiceCollection"/>. This method will not
            // register services used for pages.
            builder.RegisterControllerAndViews();

            // Registers services required by authentication services. <paramref name="defaultScheme"/> specifies the name of the
            // scheme to use by default when a specific scheme isn't requested.
            builder.AddCookieBaseAuthentication();

            // Configure logging functinality using Log4Net.
            //builder.Logging.AddLog4Net("log4net.config");
        }

        /// <summary>
        /// Application level services registered.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="builder"></param>
        public static void RegisterApplicationServices(this WebApplication app, WebApplicationBuilder builder)
        {
            // Build Service Provider static instance.
            CoditechDependencyResolver._staticServiceProvider = builder.Services.BuildServiceProvider();

            //Assign value to automapper translator
            ConfigureAutomapperServices();

            //// Adds a middleware type to the application's request pipeline.
            //app.UseMiddleware<RequestMiddleware>();

            // Adds the static file configurations with custom path.
            app.UseStaticFiles(builder);

            // Adds the <see cref="SessionMiddleware"/> to automatically enable session state for the application.
            app.UseSession();

            // Adds a <see cref="EndpointRoutingMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>.
            app.UseRouting();

            // Adds the <see cref="AuthorizationMiddleware"/> to the specified <see cref="IApplicationBuilder"/>,
            // which enables authorization capabilities.
            app.UseAuthentication();

            // Adds the<see cref= "AuthorizationMiddleware" /> to the specified < see cref = "IApplicationBuilder" />, which enables authorization capabilities.
            app.UseAuthorization();

            // Adds a route to the Microsoft.AspNetCore.Routing.IRouteBuilder with the specified
            // name and template.
            app.RegisterRoute();

            // Adds endpoints for Razor Pages to the Microsoft.AspNetCore.Routing.IEndpointRouteBuilder.
            app.MapRazorPages();
        }

        #region Common extenssion methods

        /// <summary>
        /// Adds services for controllers to the specified <see cref="IServiceCollection"/>. This method will not
        /// register services used for views or pages.
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterFilters(this WebApplicationBuilder builder)
        {

            builder.Services.AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        /// <summary>
        /// Adds services for controllers to the specified <see cref="IServiceCollection"/>. This method will not
        /// register services used for pages.
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterControllerAndViews(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews(options =>
            {
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });
        }

        /// <summary>
        ///  Enables static file serving for the given request path
        /// </summary>
        /// <param name="app"></param>
        /// <param name="builder"></param>
        public static void UseStaticFiles(this WebApplication app, WebApplicationBuilder builder)
        {
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                       Path.Combine(builder.Environment.ContentRootPath, "MediaFolder")),
                RequestPath = "/MediaFolder"
            });
        }

        /// <summary>
        ///   Adds a route to the Microsoft.AspNetCore.Routing.IRouteBuilder with the specified
        ///   name and template.
        /// </summary>
        /// <param name="app"></param>
        public static void RegisterRoute(this WebApplication app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller}/{action}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=User}/{action=Login}/{id?}");
            });
        }

        /// <summary>
        /// Assigning the values to the TanslatorExtension (Automapper)
        /// </summary>
        public static void ConfigureAutomapperServices()
        {
            // Assigned ZnoneTranslator to TranslatorExtension.
            TranslatorExtension.TranslatorInstance = CoditechDependencyResolver._staticServiceProvider?.GetService<Translator>();
        }

        /// <summary>
        /// Adds a default implementation of <see cref="IDistributedCache"/> that stores items in memory
        /// to the <see cref="IServiceCollection" />. Frameworks that require a distributed cache to work
        /// can safely add this dependency as part of their dependency list to ensure that there is at least
        /// one implementation available.
        /// </summary>
        /// <remarks>
        /// <see cref="AddDistributedMemoryCache(IServiceCollection)"/> should only be used in single
        /// server scenarios as this cache stores items in memory and doesn't expand across multiple machines.
        /// For those scenarios it is recommended to use a proper distributed cache that can expand across
        /// multiple machines.
        /// </remarks>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static void AddSession(this WebApplicationBuilder builder)
        {
            SessionStateSettings.settings = builder.Configuration.GetSection("SessionState");

            // Sql server session settings.
            //if (SessionStateSettings.EnableSQLSession)
            //    builder.Services.AddDistributedSqlServerCache(options =>
            //    {
            //        var data = SessionStateSettings.SessionTable;
            //        options.ConnectionString = SessionStateSettings.SessionConnectionString;
            //        options.SchemaName = SessionStateSettings.SessionSchema;
            //        options.TableName = SessionStateSettings.SessionTable;
            //        options.DefaultSlidingExpiration = TimeSpan.FromMinutes(SessionStateSettings.IdleTimeout);
            //        options.ExpiredItemsDeletionInterval = TimeSpan.FromMinutes(SessionStateSettings.DeletionInterval);
            //    });
            //else
                // Distributed MemoryCache session settings.
                builder.Services.AddDistributedMemoryCache();

            /// <summary>
            /// Adds services required for application session state.
            /// </summary>
            /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
            /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(Convert.ToInt16(SessionStateSettings.IdleTimeout));
                options.Cookie.IsEssential = true;
            });
        }

        public static void AddCookieBaseAuthentication(this WebApplicationBuilder builder)
        {
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
        }
    }
    #endregion
}
