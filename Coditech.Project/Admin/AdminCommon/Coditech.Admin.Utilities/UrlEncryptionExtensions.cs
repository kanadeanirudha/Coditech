using Microsoft.AspNetCore.WebUtilities;
using System.Web;

namespace Coditech.Admin.Utilities
{
    public static class UrlEncryptionExtensions
    {
        public static string EncryptedActionUrl(string basePath, object routeValues)
        {
            var query = string.Join("&",
                routeValues.GetType().GetProperties().Select(p =>
                    $"{p.Name}={HttpUtility.UrlEncode(p.GetValue(routeValues)?.ToString())}"
                )
            );

            var encrypted = EncryptionHelper.Encrypt(query);
            return $"{basePath}?data={Uri.EscapeDataString(encrypted)}";
        }
    }
}
