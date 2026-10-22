using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Text;

namespace Coditech.Common.Helper.Utilities
{
    public static class SearchHashHelper
    {
        public static string CreateSearchHash(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            string searchKey = CoditechDependencyResolver._staticServiceProvider?
                .GetService<IConfiguration>()?
                .GetSection("appsettings")["DataSearchHashKey"];

            if (string.IsNullOrWhiteSpace(searchKey))
                throw new InvalidOperationException("DataSearchHashKey must be configured.");

            string normalizedValue = value.Trim().ToUpperInvariant();
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(searchKey));
            return Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(normalizedValue)));
        }

        public static string CreateSearchTokens(string value, int minimumTokenLength = 2)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            string normalizedValue = value.Trim().ToUpperInvariant();
            var tokens = new HashSet<string>(StringComparer.Ordinal);

            for (int start = 0; start < normalizedValue.Length; start++)
            {
                for (int length = minimumTokenLength; start + length <= normalizedValue.Length; length++)
                {
                    tokens.Add(CreateSearchHash(normalizedValue.Substring(start, length)));
                }
            }

            return string.Join("|", tokens.Select(token => $"|{token}|"));
        }
    }
}
