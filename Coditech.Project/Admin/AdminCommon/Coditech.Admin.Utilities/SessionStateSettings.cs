using Microsoft.Extensions.Configuration;
namespace Coditech.Admin.Utilities
{
    public static class SessionStateSettings
    {
        /// <summary>
        /// Represents a configuration section that can be retrieved from an <see cref="IConfiguration"/> instance.
        /// </summary>
        public static IConfigurationSection settings;
        
        /// <summary>
        /// Gets the SessionConnectionString from the settings.
        /// </summary>
        public static string SessionConnectionString
        {
            get
            {
                return settings["SessionConnectionString"]!;
            }
        }
        /// <summary>
        /// Gets the name of the session table.
        /// </summary>
        /// <returns>The name of the session table.</returns>
        public static string SessionTable
        {
            get
            {
                return settings["SessionTableName"]!;
            }
        }
        /// <summary>
        /// Gets the SessionSchemaName from the settings.
        /// </summary>
        public static string SessionSchema
        {
            get
            {
                return settings["SessionSchemaName"]!;
            }
        }
        /// <summary>
        /// Gets the idle timeout value from the settings.
        /// </summary>
        /// <returns>The idle timeout value.</returns>
        public static int IdleTimeout
        {
            get
            {
                return Convert.ToInt16(settings["IdleTimeout"]);
            }
        }
        /// <summary>
        /// Gets the DeletionInterval from the settings.
        /// </summary>
        /// <returns>The DeletionInterval as an integer.</returns>
        public static int DeletionInterval
        {
            get
            {
                return Convert.ToInt16(settings["DeletionInterval"]);
            }
        }
        /// <summary>
        /// Gets a value indicating whether SQL session is enabled.
        /// </summary>
        /// <returns>A boolean value indicating whether SQL session is enabled.</returns>
        public static bool EnableSQLSession
        {
            get
            {
                return Convert.ToBoolean(settings["EnableSQLSession"]);
            }
        }

    }

}
