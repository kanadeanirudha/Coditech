using Coditech.Admin.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
namespace Coditech.Admin.Middleware
{
    public class EncryptedQueryMiddleware
    {
        private readonly RequestDelegate _next;

        public EncryptedQueryMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // If encryption Is disable , then return plain text
            if (CoditechAdminSettings.IsURLEncrypted && context.Request.Query.ContainsKey("data"))
            {
                try
                {
                    string encryptedQueryString = context.Request.Query["data"];
                    string decryptedQueryString = EncryptionHelper.Decrypt(encryptedQueryString!);

                    var dict = QueryHelpers.ParseQuery(decryptedQueryString);
                    var queryCollection = new QueryCollection(dict.ToDictionary(
                        kvp => kvp.Key,
                        kvp => new Microsoft.Extensions.Primitives.StringValues(kvp.Value.ToArray())
                    ));

                    context.Request.Query = queryCollection;
                }
                catch { }
            }

            context.Response.OnStarting(() =>
            {
                if (CoditechAdminSettings.IsURLEncrypted &&
                    context.Response.StatusCode == 302 &&
                    context.Response.Headers.ContainsKey("Location"))
                {
                    string location = context.Response.Headers["Location"].ToString();
                    var uri = new Uri(location, UriKind.RelativeOrAbsolute);
                    if (uri.IsAbsoluteUri && !string.IsNullOrEmpty(uri.Query))
                    {
                        string query = uri.Query.TrimStart('?');
                        string encrypted = EncryptionHelper.Encrypt(query);
                        string newUrl = uri.GetLeftPart(UriPartial.Path) + "?data=" + Uri.EscapeDataString(encrypted);
                        context.Response.Headers["Location"] = newUrl;
                    }
                }
                return Task.CompletedTask;
            });

            await _next(context);
        }

    }
}
