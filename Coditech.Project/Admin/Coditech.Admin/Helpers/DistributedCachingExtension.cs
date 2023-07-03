using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Coditech.Admin.Helpers
{
    public static class DistributedCachingExtension
    {
        /// <summary>
        ///  Register Caching in the application.
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterCaching(this WebApplicationBuilder builder)
        {
            //CoditechCacheSettings.settings = builder.Configuration.GetSection("Caching");
            //string cachingType = CoditechCacheSettings.CachingType;

            //switch (cachingType)
            //{
            //    // Register Sql cache
            //    case "DistributedSQLServerCache":
            //        builder.Services.AddDistributedSqlServerCache(options =>
            //        {
            //            options.ConnectionString = CoditechCacheSettings.SqlCacheConnectionString;
            //            options.SchemaName = CoditechCacheSettings.CacheSchema;
            //            options.TableName = CoditechCacheSettings.CacheTable;
            //            options.DefaultSlidingExpiration = TimeSpan.FromMinutes(CoditechCacheSettings.SlidingExpiration);
            //            options.ExpiredItemsDeletionInterval = TimeSpan.FromMinutes(CoditechCacheSettings.DeletionInterval);
            //        });
            //        builder.Services.AddTransient<ICoditechCacheProvider, CoditechSQLServerCache>();
            //        break;

            //    // Register redis cache
            //    case "CoditechRedisCache":
            //        builder.Services.AddStackExchangeRedisCache(options =>
            //        {
            //            options.Configuration = "ConnectionString:Redis";
            //            options.InstanceName = "SampleInstance";
            //        });
            //        builder.Services.AddTransient<ICoditechCacheProvider, CoditechRedisCache>();
            //        break;

            //    // Register InMemory cache
            //    default:
            //        builder.Services.AddTransient<ICoditechCacheProvider, CoditechInMemoryCache>();
            //        break;
            //}

        }
    }
}
