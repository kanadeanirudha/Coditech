using Coditech.Common.Helper;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coditech.Common.API
{
    public static class ApiSettings
    {
        private static IConfigurationSection settings = CoditechDependencyResolver._staticServiceProvider?.GetService<IConfiguration>().GetSection("appsettings");

        public static void SetConfigurationSettingSource(IConfigurationSection settingSource)
        {
            settings = settingSource;
        }

        public static string ApiRootUri
        {
            get
            {
                return Convert.ToString(settings["ApiRootUri"]);
            }
        }
        public static string EnableFileLogging
        {
            get
            {
                return Convert.ToString(settings["EnableFileLogging"]);
            }
        }
        public static string EnableDBLogging
        {
            get
            {
                return Convert.ToString(settings["EnableDBLogging"]);
            }
        }
        public static string ValidateRequestAPI
        {
            get
            {
                return Convert.ToString(settings["ValidateRequestAPI"]);
            }
        }

        public static string CoditechApiDomainName
        {
            get
            {
                return Convert.ToString(settings["CoditechApiDomainName"]);
            }
        }

        public static string CoditechApiDomainKey
        {
            get
            {
                return Convert.ToString(settings["CoditechApiDomainKey"]);
            }
        }

        public static bool MinifiedJsonResponse
        {
            get
            {
                return HttpHeaders.GetHeaderValue(HttpHeaders.Header_MinifiedJsonResponse).TryParseBoolean();
            }
        }

        public static string CoditechApiUriKeyValueSeparator
        {
            get
            {
                return Convert.ToString(settings["CoditechApiUriKeyValueSeparator"]);
            }
        }
        public static string CoditechApiUriItemSeparator
        {
            get
            {
                return Convert.ToString(settings["CoditechApiUriItemSeparator"]);
            }
        }
        public static string CoditechCommaReplacer
        {
            get
            {
                return Convert.ToString(settings["CoditechCommaReplacer"]);
            }
        }
        public static string ResetPasswordExpriedTimeInMinute
        {
            get
            {
                return Convert.ToString(settings["ResetPasswordExpriedTimeInMinute"]);
            }
        }
        public static string ApiDomainRequestKey
        {
            get
            {
                return Convert.ToString(settings["ApiDomainRequestKey"]);
            }
        }
        public static byte JoiningCodeLength
        {
            get
            {
                return Convert.ToByte(settings["JoiningCodeLength"]);

            }
        }
       
        public static long ApiRequestTimeout
        {
            get
            {
                return Convert.ToInt64(settings["ApiRequestTimeout"]);

            }
        }
        public static short LogMessageRetentionPeriodTimeInDays
        {
            get
            {
                return Convert.ToByte(settings["LogMessageRetentionPeriodTimeInDays"]);
            }
        }
    }
}
