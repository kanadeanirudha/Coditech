using Coditech.Common.Helper;

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

        public static string CoditechOrganisationApiRootUri
        {
            get
            {
                if (settings["EnvironmentName"] == "dev")
                    return Convert.ToString(settings["CoditechOrganisationApiRootUri"]);
                else
                    return Convert.ToString($"{settings["Scheme"]}{settings["ClientName"]}-{settings["EnvironmentName"]}-api-organisation.{settings["ApiDomainName"]}");
            }
        }

        public static string CoditechMediaManagerApiRootUri
        {
            get
            {
                if (settings["EnvironmentName"] == "dev")
                    return Convert.ToString(settings["CoditechMediaManagerApiRootUri"]);
                else
                    return Convert.ToString($"{settings["Scheme"]}{settings["ClientName"]}-{settings["EnvironmentName"]}-api-mediamanager.{settings["ApiDomainName"]}");
            }
        }

        public static string CoditechPaymentApiRootUri
        {
            get
            {
                if (settings["EnvironmentName"] == "dev")
                    return Convert.ToString(settings["CoditechPaymentApiRootUri"]);
                else
                    return Convert.ToString($"{settings["Scheme"]}{settings["ClientName"]}-{settings["EnvironmentName"]}-api-payment.{settings["ApiDomainName"]}");
            }
        }

        public static string CoditechGazetteApiRootUri
        {
            get
            {
                if (settings["EnvironmentName"] == "dev")
                    return Convert.ToString(settings["CoditechGazetteApiRootUri"]);
                else
                    return Convert.ToString($"{settings["Scheme"]}{settings["ClientName"]}-{settings["EnvironmentName"]}-api-gazette.{settings["ApiDomainName"]}");
            }
        }

        public static string CoditechHospitalManagementSystemApiRootUri
        {
            get
            {
                if (settings["EnvironmentName"] == "dev")
                    return Convert.ToString(settings["CoditechHospitalManagementSystemApiRootUri"]);
                else
                    return Convert.ToString($"{settings["Scheme"]}{settings["ClientName"]}-{settings["EnvironmentName"]}-api-hospitalmanagementsystem.{settings["ApiDomainName"]}");
            }
        }
        public static string ApplicationLogoPath
        {
            get
            {
                if (settings["EnvironmentName"] == "dev")
                    return Convert.ToString(settings["CoditechMediaManagerApiRootUri"] + settings["ApplicationLogoPath"]);
                else
                    return Convert.ToString($"{settings["Scheme"]}{settings["ClientName"]}-{settings["EnvironmentName"]}-api-mediamanager.{settings["ApiDomainName"]}{settings["ApplicationLogoPath"]}");
            }
        }

        public static string ApplicationLogoSmallPath
        {
            get
            {
                if (settings["EnvironmentName"] == "dev")
                    return Convert.ToString(settings["CoditechMediaManagerApiRootUri"] + settings["ApplicationLogoSmallPath"]);
                else
                    return Convert.ToString($"{settings["Scheme"]}{settings["ClientName"]}-{settings["EnvironmentName"]}-api-mediamanager.{settings["ApiDomainName"]}{settings["ApplicationLogoSmallPath"]}");
            }
        }

        public static string NotificationMessagesIsFadeOut
        {
            get
            {
                return Convert.ToString(settings["NotificationMessagesIsFadeOut"]);
            }
        }

        public static string ApplicationTitle
        {
            get
            {
                return Convert.ToString(settings["ApplicationTitle"]);
            }
        }
        public static string ApplicationLayoutBackground
        {
            get
            {
                return Convert.ToString(settings["ApplicationLayoutBackground"]);
            }
        }
        public static string Scheme
        {
            get
            {
                return Convert.ToString(settings["Scheme"]);
            }
        }

        public static string ApiDomainName
        {
            get
            {
                return Convert.ToString(settings["ApiDomainName"]);
            }
        }
        public static string IsDefaultTheme
        {
            get
            {
                return Convert.ToString(settings["IsDefaultTheme"]);
            }
        }
        public static string ThemeColor
        {
            get
            {
                return Convert.ToString(settings["ThemeColor"]);
            }
        }
        public static short DefaultDashboardDataDays
        {
            get
            {
                return Convert.ToInt16(settings["DefaultDashboardDataDays"]);
            }
        }
        public static string DashboardDays
        {
            get
            {
                return Convert.ToString(settings["DashboardDays"]);
            }
        }
        public static bool MaintenanceMode
        {
            get
            {
                return Convert.ToBoolean(settings["MaintenanceMode"]);
            }
        }
        public static string ApplicationCode
        {
            get
            {
                return Convert.ToString(settings["ApplicationCode"]);
            }
        }
        public static string DashboardUrl
        {
            get
            {
                return Convert.ToString(settings["DashboardUrl"]);
            }
        }
        public static string EncryptionIV
        {
            get
            {
                return Convert.ToString(settings["EncryptionIV"]);

            }
        }
        public static string EncryptionKey
        {
            get
            {
                return Convert.ToString(settings["EncryptionKey"]);

            }
        }
        public static bool IsEncryption
        {
            get
            {
                return Convert.ToBoolean(settings["IsEncryption"]);

            }
        }
    }
}
