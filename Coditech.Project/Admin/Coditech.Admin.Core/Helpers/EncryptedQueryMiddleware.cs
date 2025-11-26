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
                catch
                {
                    // Redirect on bad encrypted URL
                    context.Response.Redirect("/User/PageNotFoundRequest");
                    return;
                }
            }

            context.Response.OnStarting(() =>
            {
                if (CoditechAdminSettings.IsURLEncrypted &&
                    context.Response.StatusCode == 302 &&
                    context.Response.Headers.ContainsKey("Location"))
                {
                    string location = context.Response.Headers["Location"].ToString();
                    var uri = new Uri(location, UriKind.RelativeOrAbsolute);

                    // Use the current decrypted request query
                    var queryDict = context.Request.Query.ToDictionary(
                         kv => kv.Key,
                         kv => string.Join(",", kv.Value)
                    );

                    if (queryDict.Any())
                    {
                        string plainQueryString = string.Join("&", queryDict.Select(kvp => $"{kvp.Key}={kvp.Value}"));
                        string encrypted = EncryptionHelper.Encrypt(plainQueryString);

                        string redirectBase = uri.IsAbsoluteUri
                            ? uri.GetLeftPart(UriPartial.Path)
                            : location.Split('?')[0];

                        string newUrl = redirectBase + "?data=" + Uri.EscapeDataString(encrypted);
                        context.Response.Headers["Location"] = newUrl;
                    }
                }
                return Task.CompletedTask;
            });

            await _next(context);
        }

    }
}
