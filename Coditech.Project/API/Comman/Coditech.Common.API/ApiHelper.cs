using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

namespace Coditech.Common.API
{
    public class ApiHelper
    {
        private readonly IServiceProvider _serviceProvider;
        public ApiHelper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //This method respects the http header value to optimize JSON response from APIs
        //Sets the Newtonsoft JSON conversion setting, to minify the serialized JSON to ignore null and default value properties.
        //So each API response which we are serializing using this method will use these settings.
        public static string ToJson(object data)
        {
            bool minifiedJsonResponse = false;// ApiSettings.MinifiedJsonResponse;
            return JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings
            {
                DefaultValueHandling = minifiedJsonResponse ? DefaultValueHandling.Ignore : DefaultValueHandling.Include
            });
        }
    }
}
