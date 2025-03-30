using Coditech.Common.API;
using Coditech.Common.Helper;
using Coditech.Common.Logger;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Coditech.Hangfire
{
    public static class HangfireStartup
    {
        public static IConfiguration configuration = CoditechDependencyResolver.GetService<IConfiguration>();

        //This method is called by runtime. Use this method to add the servcies to the conatiner.
        public static void ConfigureServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            string connectionString = GetHangfireConnectionString(configurationManager);

            IConfigurationSection configSection = configurationManager.GetSection(HangfireConstants.HangfireConfigSection);

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration.UseSqlServerStorage(connectionString, new SqlServerStorageOptions
            {

                CommandBatchMaxTimeout = TimeSpan.FromMinutes(Convert.ToInt32(configSection[HangfireConstants.HangfireCommandBatchMaxTimeout])),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(Convert.ToInt32(configSection[HangfireConstants.HangfireSlidingInvisibilityTimeout])),
                QueuePollInterval = TimeSpan.FromMilliseconds(Convert.ToInt32(configSection[HangfireConstants.HangfireQueuePollInterval])),
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true,
                PrepareSchemaIfNecessary = Convert.ToBoolean(configSection[HangfireConstants.HangfirePrepareSchemaIfNecessary]),
            }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            // Add controllers.
            services.AddControllers();

        }

        //Basic Authentication added to access the Hangfire Dashboard  
        public static void ConfigureHangfireDashboard(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                AppPath = null,
                StatsPollingInterval = HangfireSettings.HangfireStatsPollingInterval,
                IsReadOnlyFunc = ((DashboardContext _) => HangfireSettings.MakeHangfireDashboardReadOnly),
                Authorization = new IDashboardAuthorizationFilter[]
                        {
                            new BasicAuthAuthorizationFilter(
                                new BasicAuthAuthorizationFilterOptions
                                {
                                    LoginCaseSensitive = true,
                                    Users = GetAuthorizedUserCredentials()
                                })
                        }
            });
        }

        private static BasicAuthAuthorizationUser[] GetAuthorizedUserCredentials()
        {
            List<BasicAuthAuthorizationUser> credentials = new List<BasicAuthAuthorizationUser>();
            try
            {
                Dictionary<string, string> credsFromConfig = HangfireSettings.HangfireDashboardCredentials;

                foreach (var credPair in credsFromConfig)
                {
                    credentials.Add(new BasicAuthAuthorizationUser()
                    {
                        Login = credPair.Key,
                        PasswordClear = credPair.Value
                    });
                }
                return credentials.ToArray();
            }
            catch (Exception ex)
            {
                new CoditechLogging().LogMessage(ex, CoditechLoggingEnum.Components.Hangfire.ToString(), TraceLevel.Error);
                new CoditechLogging().LogMessage(ex, "Hangfire", System.Diagnostics.TraceLevel.Info);
                return credentials.ToArray();
            }
        }

        /// <summary>
        /// Gets the hangfire connection string from the respective application settings.
        /// </summary>
        /// <param name="configurationManager">ConfigurationManager</param>
        /// <returns>Connection string</returns>
        private static string GetHangfireConnectionString(ConfigurationManager configurationManager)
            => configurationManager?.GetConnectionString(HangfireConstants.HangfireConnection);
    }
}

