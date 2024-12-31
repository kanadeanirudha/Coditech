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
#if DEBUG
                return Convert.ToString(settings["CoditechOrganisationApiRootUri"]);
#else
                return Convert.ToString($"{settings["Scheme"]}organisation.{settings["ApiDomainName"]}");
#endif
            }
        }

        public static string CoditechPaymentApiRootUri
        {
            get
            {
#if DEBUG
                return Convert.ToString(settings["CoditechPaymentApiRootUri"]);
#else
                return Convert.ToString($"{settings["Scheme"]}payment.{settings["ApiDomainName"]}");
#endif
            }
        }

        public static string CoditechGymManagementSystemApiRootUri
        {
            get
            {
#if DEBUG
                return Convert.ToString(settings["CoditechGymManagementSystemApiRootUri"]);
#else
                return Convert.ToString($"{settings["Scheme"]}gymmanagementsystem.{settings["ApiDomainName"]}");
#endif
            }
        }
        public static string CoditechInventoryApiRootUri
        {
            get
            {
#if DEBUG
                return Convert.ToString(settings["CoditechInventoryApiRootUri"]);
#else
                return Convert.ToString($"{settings["Scheme"]}inventory.{settings["ApiDomainName"]}");
#endif
            }
        }
        public static string CoditechGazetteApiRootUri
        {
            get
            {
#if DEBUG
                return Convert.ToString(settings["CoditechGazetteApiRootUri"]);
#else
                return Convert.ToString($"{settings["Scheme"]}gazette.{settings["ApiDomainName"]}");
#endif
            }
        }
        public static string CoditechMediaManagerApiRootUri
        {
            get
            {
#if DEBUG
                return Convert.ToString(settings["CoditechMediaManagerApiRootUri"]);
#else
                return Convert.ToString($"{settings["Scheme"]}mediamanager.{settings["ApiDomainName"]}");
#endif
            }
        }

        public static string CoditechHospitalManagementSystemApiRootUri
        {
            get
            {
#if DEBUG
                return Convert.ToString(settings["CoditechHospitalManagementSystemApiRootUri"]);
#else
                return Convert.ToString($"{settings["Scheme"]}hospitalmanagementsystem.{settings["ApiDomainName"]}");
#endif
            }
        }
        public static string NotificationMessagesIsFadeOut
        {
            get
            {
                return Convert.ToString(settings["NotificationMessagesIsFadeOut"]);
            }
        }
        public static string ApplicationLogoPath
        {
            get
            {
#if DEBUG
                return Convert.ToString(settings["CoditechMediaManagerApiRootUri"] + settings["ApplicationLogoPath"]);
#else
                return Convert.ToString($"{settings["Scheme"]}mediamanager.{settings["ApiDomainName"]}{settings["ApplicationLogoPath"]}");
#endif
            }
        }
        public static string CoditechDBTMApiRootUri
        {
            get
            {
#if DEBUG
                return Convert.ToString(settings["CoditechDBTMApiRootUri"]);
#else
                return Convert.ToString($"{settings["Scheme"]}DBTM.{settings["ApiDomainName"]}");
#endif
            }
        }
        public static string ApplicationLogoBackground
        {
            get
            {
                return Convert.ToString(settings["ApplicationLogoBackground"]);
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
    }
}
