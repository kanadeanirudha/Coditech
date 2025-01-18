using Coditech.Admin.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace Coditech.Admin.Helpers
{
    public static class AddSessionState
    {
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

            //// Sql server session settings.
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
                options.IdleTimeout = TimeSpan.FromMinutes(SessionStateSettings.IdleTimeout);
                options.Cookie.IsEssential = true;
            });
        }

    }
}
