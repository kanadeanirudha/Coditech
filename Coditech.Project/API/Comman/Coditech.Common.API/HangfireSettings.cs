using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Coditech.Common.API
{
    public static class HangfireSettings
    {
        private static IConfiguration settings = CoditechDependencyResolver._staticServiceProvider.GetService<IConfiguration>();

        public static bool HangfirePrepareSchemaIfNecessary => Convert.ToBoolean(settings.GetSection("HangfireConfigSection:HangfirePrepareSchemaIfNecessary").Value);

        public static int HangfireCommandBatchMaxTimeout => Convert.ToInt32(settings.GetSection("HangfireConfigSection:HangfireCommandBatchMaxTimeout").Value);

        public static int HangfireSlidingInvisibilityTimeout => Convert.ToInt32(settings.GetSection("HangfireConfigSection:HangfireSlidingInvisibilityTimeout").Value);

        public static int HangfireQueuePollInterval => Convert.ToInt32(settings.GetSection("HangfireConfigSection:HangfireQueuePollInterval").Value);

        public static bool EnableHangfireDashboard => Convert.ToBoolean(settings.GetSection("HangfireConfigSection:EnableHangfireDashboard").Value);

        public static int HangfireStatsPollingInterval
        {
            get
            {
                int num = Convert.ToInt32(settings.GetSection("HangfireConfigSection:HangfireStatsPollingInterval").Value);
                if (num != 0)
                {
                    return num * 60000;
                }

                return 300000;
            }
        }

        public static bool MakeHangfireDashboardReadOnly => Convert.ToBoolean(settings.GetSection("HangfireConfigSection:MakeHangfireDashboardReadOnly").Value);

        public static Dictionary<string, string> HangfireDashboardCredentials
        {
            get
            {
                string? value = settings.GetSection("HangfireConfigSection:HangfireDashboardCredentials").Value;
                if (string.IsNullOrEmpty(value) && EnableHangfireDashboard)
                {
                    LogAndThrow();
                }

                string[] array = value.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                if (array.Length != 0)
                {
                    string[] array2 = array;
                    for (int i = 0; i < array2.Length; i++)
                    {
                        string[] array3 = array2[i].Split(new char[1] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        if (array3.Length == 2 && !dictionary.ContainsKey(array3[0].Trim()))
                        {
                            dictionary.Add(array3[0].Trim(), array3[1].Trim());
                        }
                        else
                        {
                            LogAndThrow();
                        }
                    }
                }

                return dictionary;
            }
        }

        private static void LogAndThrow()
        {
            string text = "Hangfire dashboard login credentials not configured properly in the API web.config";
            CoditechLoggingHelper.CoditechLogging.LogMessage(text, "CookieHelper", TraceLevel.Error);
            throw new CoditechException(5, text);
        }
    }
}
