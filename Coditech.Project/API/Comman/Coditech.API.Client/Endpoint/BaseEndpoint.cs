using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Coditech.API.Client.Endpoint
{
    public abstract class BaseEndpoint
    {
        private string _localeId;
        private string LocaleHeader
        {
            get
            {
                return "Locale: " + _localeId;
            }

            set { _localeId = value; }
        }

        private static IConfigurationSection settings = CoditechDependencyResolver.GetService<IConfiguration>().GetSection("appsettings");
        private string UriItemSeparator => settings["CoditechApiUriItemSeparator"];
        private string UriKeyValueSeparator => settings["CoditechApiUriKeyValueSeparator"];
        private string CommaReplacer => settings["CoditechCommaReplacer"];
        
        protected string BuildEndpointQueryString(IEnumerable<string> expand = null, IEnumerable<FilterTuple> filter = null, IDictionary<string, string> sort = null, int? pageIndex = null, int? pageSize = null, params string[] param) =>
        string.Concat(BuildExpandQueryString(expand), BuildFilterQueryString(filter), BuildSortQueryString(sort), BuildPageIndexQueryString(pageIndex), BuildPageSizeQueryString(pageSize), CustomEndpoint(param));

        private string BuildExpandQueryString(IEnumerable<string> expands)
        {
            string queryString = "?expand=";

            if (expands != null)
            {
                foreach (string e in expands)
                    queryString += e + UriItemSeparator;

                queryString = queryString.TrimEnd(UriItemSeparator.ToCharArray());
            }

            return queryString;
        }
        private string BuildFilterQueryString(IEnumerable<FilterTuple> filters)
        {
            string queryString = "&filter=";

            if (filters != null)
            {
                foreach (FilterTuple f in filters)
                    queryString += $"{f.FilterName}{UriKeyValueSeparator}{f.FilterOperator}{UriKeyValueSeparator}{HttpUtility.UrlEncode(f.FilterValue?.Replace(",", CommaReplacer))}{UriItemSeparator}";

                queryString = queryString.TrimEnd(UriItemSeparator.ToCharArray());
            }

            return queryString;

        }

        private string BuildSortQueryString(IDictionary<string, string> sorts)
        {
            string queryString = "&sort=";

            if (sorts != null)
            {
                foreach (KeyValuePair<string, string> s in sorts)
                    queryString += $"{s.Key}{UriKeyValueSeparator}{s.Value}{UriItemSeparator}";

                queryString = queryString.TrimEnd(UriItemSeparator.ToCharArray());
            }

            return queryString;

        }
        private string BuildPageIndexQueryString(int? pageIndex)
        {

            if (pageIndex.HasValue)
            {
                string queryString = "&pageIndex=";
                queryString += $"{pageIndex.Value}";
                return queryString;
            }
            return string.Empty;
        }

        private string BuildPageSizeQueryString(int? pageSize)
        {

            if (pageSize.HasValue)
            {
                string queryString = "&pageSize=";
                queryString += $"{pageSize.Value}";
                return queryString;
            }
            return string.Empty;
        }


        private string CustomEndpoint(params string[] param)
        {
            string endpoint = string.Empty;
            if (param == null || param.FirstOrDefault() == null)
                return endpoint;
            foreach (var parameter in param)
            {
                if (parameter.Contains("cache"))
                {
                    endpoint += BuildCacheRefreshQueryInString(endpoint);
                }
                else if (parameter.Contains("locale"))
                {
                    endpoint += BuildLocaleQueryInString(parameter);
                }
                else if (parameter.Contains(Convert.ToString(settings["EndpointSplitter"])))
                {
                    var stringArray = parameter.Split(Convert.ToString(settings["EndpointSplitter"]));
                    endpoint += BuildCustomEndpointQueryInString(parameter, stringArray[0], stringArray[1]);
                }
                else
                {
                    return endpoint;
                }
            }

            return endpoint;
        }

        private string BuildCacheRefreshQueryInString(string endpoint) => endpoint + "&cache=refresh";
        private string BuildLocaleQueryInString(string endpoint)
        => endpoint + "&locale=" + _localeId;

        private string BuildCustomEndpointQueryInString(string endpoint, string key, string value)
        => endpoint + $"&{key}=" + HttpUtility.UrlEncode(value);

    }
}
