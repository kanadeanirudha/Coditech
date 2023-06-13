using System.Text;

namespace Coditech.Common.Helper
{
    public class HttpHeaders
    {
        public const string Header_MinifiedJsonResponse = "Minified-Json-Response";

        public static string GetHeaderFormattedString(string header, string value)
        {
            return new StringBuilder(header).Append(": ").Append(value).ToString();
        }

        public static string GetHeaderValue(string header)
        {
            return HttpContextHelper.Current.Request.Headers[header];
        }
    }
}
