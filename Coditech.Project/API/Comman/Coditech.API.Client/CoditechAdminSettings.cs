using Coditech.Common.Helper;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coditech.Client
{
    public static class CoditechAdminSettings
    {
        private static IConfigurationSection settings = CoditechDependencyResolver.GetService<IConfiguration>().GetSection("appsettings");
        public static void SetConfigurationSettingSource(IConfigurationSection settingSource)
        {
            settings = settingSource;
        }
        public static string CoditechApiRootUri
        {
            get
            {
                return Convert.ToString(settings["CoditechApiRootUri"]);
            }
        }
    }
}
