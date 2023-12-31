﻿using Coditech.Common.Helper;

using Microsoft.Extensions.Configuration;

namespace Coditech.Admin.Utilities
{
    public static class CoditechAdminSettings
    {
        /// <summary>
        /// Gets the appsettings configuration section.
        /// </summary>
        /// <returns>The appsettings configuration section.</returns>
        private static IConfigurationSection settings = CoditechDependencyResolver.GetService<IConfiguration>().GetSection("appsettings");

        /// <summary>
        /// Sets the configuration setting source.
        /// </summary>
        /// <param name="settingSource">The setting source.</param>
        public static void SetConfigurationSettingSource(IConfigurationSection settingSource)
        {
            settings = settingSource;
        }

        public static bool IsCookieHttpOnly
        {
            get
            {
                return Convert.ToBoolean(settings["IsCookieHttpOnly"]);
            }
        }
        public static bool IsCookieSecure
        {
            get
            {
                return Convert.ToBoolean(settings["IsCookieSecure"]);
            }
        }
        public static string CookieExpiresValue
        {
            get
            {
                return Convert.ToString(settings["CookieExpiresValue"]);
            }
        }
        public static string CoditechUserApiRootUri
        {
            get
            {
                return Convert.ToString(settings["CoditechUserApiRootUri"]);
            }
        }
        public static string CoditechOrganisationApiRootUri
        {
            get
            {
                return Convert.ToString(settings["CoditechOrganisationApiRootUri"]);
            }
        }
        public static string CoditechEmployeeApiRootUri
        {
            get
            {
                return Convert.ToString(settings["CoditechEmployeeApiRootUri"]);
            }
        }
        public static string CoditechGymManagementSystemApiRootUri
        {
            get
            {
                return Convert.ToString(settings["CoditechGymManagementSystemApiRootUri"]);
            }
        }
        public static string CoditechInventoryApiRootUri
        {
            get
            {
                return Convert.ToString(settings["CoditechInventoryApiRootUri"]);
            }
        }
        public static string CoditechMediaManagerApiRootUri
        {
            get
            {
                return Convert.ToString(settings["CoditechMediaManagerApiRootUri"]);
            }
        }
        public static string NotificationMessagesIsFadeOut
        {
            get
            {
                return Convert.ToString(settings["NotificationMessagesIsFadeOut"]);
            }
        }
    }
}
