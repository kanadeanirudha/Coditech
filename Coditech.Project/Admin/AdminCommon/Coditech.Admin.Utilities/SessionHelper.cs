using Coditech.Admin.Utilities;
using Coditech.Common.Helper;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using System.Dynamic;

using static Coditech.Common.Helper.CoditechDependencyResolver;



namespace Coditech.Admin.Utilities
{
    public static class SessionHelper
    {

        // This line of code creates a private static readonly variable called 'cache' and assigns it to an instance of the IDistributedCache interface. This interface provides access to a distributed cache, which is a type of data storage that allows for the storage and retrieval of data across multiple servers.
        private static readonly IDistributedCache cache = GetService<IDistributedCache>();

        /// <summary>
        /// Saves the data in session.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void SaveDataInSession<T>(string key, T value)
        {
            RemoveDataFromSession(key);
            HttpContext context = _staticServiceProvider?.GetService<IHttpContextAccessor>()?.HttpContext;
            //only InProc session mode will support CLR objects, hence no need of object serialization
            //but other modes (SQL or State server or any custom) may not support storing CLR objects, so in those cases serialization would be required
            switch (GetSessionStateMode())
            {
                case SessionStateMode.InProc:
                    if (HelperUtility.IsNotNull(context.Session))
                        context.Session.SetString(key, JsonConvert.SerializeObject(value));
                    break;

                default:
                    SaveInSessionByDataType(key, value);
                    break;
            }
        }

        /// <summary>
        /// Gets the data from session.
        /// </summary>
        /// <typeparam name="T">The type of the data.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The data from session.</returns>
        public static T GetDataFromSession<T>(string key)
        {
            //only InProc session mode will support CLR objects, hence no need of object deserialization
            //but other modes (SQL or State server or any custom) may not support storing CLR objects, so in those cases deserialization would be required
            HttpContext context = _staticServiceProvider?.GetService<IHttpContextAccessor>()?.HttpContext;
            switch (GetSessionStateMode())
            {
                case SessionStateMode.InProc:
                    var o = context.Session.GetString(key);
                    if (!string.IsNullOrEmpty(o))
                    {
                        if (typeof(T) == typeof(List<dynamic>))
                        {
                            ApplicationSessionConfiguration applicationSessionConfiguration = new ApplicationSessionConfiguration();
                            return (T)(object)applicationSessionConfiguration.GetDeSerializeExpandoData(o!);
                        }
                        else
                            return JsonConvert.DeserializeObject<T>(o, new DecimalConverter())!;
                    }
                    break;

                default:
                    // for other modes(SQL or State server or any custom) and generic data type is list of dynamic ,conditional deserialization would be required. 
                    if (typeof(T) == typeof(List<dynamic>))
                    {
                        ApplicationSessionConfiguration applicationSessionConfiguration = new ApplicationSessionConfiguration();
                        return (T)(object)applicationSessionConfiguration.GetDeSerializeExpandoData(cache.GetString(key)!);
                    }
                    else
                    {
                        ApplicationSessionConfiguration applicationSessionConfiguration = new ApplicationSessionConfiguration();
                        return applicationSessionConfiguration.GetDeSerializeData<T>(cache.GetString(key)!);
                    }
            }

            //NOTE: We can't return default, it may have negative effect on if "T" is bool which will always be false, also int may always return 0 as default
            //but this method have been called on many places to check existence rather then just "get", but then how we can return type T?

            return default(T);
        }

        /// <summary>
        /// Removes data from the session based on the given key.
        /// </summary>
        /// <param name="key">The key of the data to be removed.</param>
        public static void RemoveDataFromSession(string key)
        {
            HttpContext context = _staticServiceProvider?.GetService<IHttpContextAccessor>()?.HttpContext;
            switch (GetSessionStateMode())
            {
                case SessionStateMode.InProc:
                    var obj = GetDataFromSession<object>(key);
                    if (HelperUtility.IsNull(obj)) return;
                    cache.Remove(key);
                    context.Session.Remove(key);
                    break;

                default:
                    cache.Remove(key);
                    break;
            }
        }

        //This method handle grid filter related issue, due to list of dynamic type unable to deserialize from saved data in session, when session state is sql server.
        private static void SaveInSessionByDataType<T>(string key, T value)
        {
            ApplicationSessionConfiguration applicationSessionConfiguration = new ApplicationSessionConfiguration();
            string sessionValue = string.Empty;
            //Only in other mode(SQL or State server or any custom) and generic type is list of dynamic.
            if (typeof(T) == typeof(List<dynamic>))
            {
                List<ExpandoObject> listExpObj = ((List<dynamic>)(object)value).Select(d => d as ExpandoObject).ToList();
                sessionValue = applicationSessionConfiguration.GetSerializedData<List<ExpandoObject>>(listExpObj);
            }
            else
            {
                sessionValue = applicationSessionConfiguration.GetSerializedData<T>(value);
            }
            //Set Sql session by using DistributedCache.
            cache.SetString(key, sessionValue);
            //context.Session.SetString(key, sessionValue);
        }

        /// <summary>
        /// Gets the session state mode based on the SessionStateSettings.EnableSQLSession flag.
        /// </summary>
        /// <returns>The session state mode (InProc or SQLServer).</returns>
        public static SessionStateMode GetSessionStateMode()
        {
            return SessionStateSettings.EnableSQLSession ? SessionStateMode.SQLServer : SessionStateMode.InProc;
        }

        /// <summary>
        /// Checks if the session object is present.
        /// </summary>
        /// <returns>True if the session object is present, false otherwise.</returns>
        public static bool IsSessionObjectPresent()
        {
            HttpContext context = _staticServiceProvider?.GetService<IHttpContextAccessor>()?.HttpContext;
            return !Equals(context?.Session, null);
        }


        public static void Clear()
        {
            HttpContext context = _staticServiceProvider?.GetService<IHttpContextAccessor>()?.HttpContext;
            context.Session.Clear();
        }

        /// <summary>
        /// Gets the session ID from the current HttpContext.
        /// </summary>
        /// <returns>The session ID.</returns>
        public static string GetSessionId()
        {
            HttpContext context = CoditechDependencyResolver._staticServiceProvider?.GetService<IHttpContextAccessor>()?.HttpContext;
            return context.Session.Id;
        }
    }

    /// <summary>
    /// Enum to specify the mode of session state.
    /// </summary>
    public enum SessionStateMode
    {
        //
        // Summary:
        //     Session state is disabled.
        Off = 0,
        //
        // Summary:
        //     Session state is in process with an ASP.NET worker process.
        InProc = 1,
        //
        // Summary:
        //     Session state is using the out-of-process ASP.NET State Service to store state
        //     information.
        StateServer = 2,
        //
        // Summary:
        //     Session state is using an out-of-process SQL Server database to store state information.
        SQLServer = 3,
        //
        // Summary:
        //     Session state is using a custom data store to store session-state information.
        Custom = 4
    }
}