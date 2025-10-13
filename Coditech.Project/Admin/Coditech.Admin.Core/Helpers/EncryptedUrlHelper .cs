using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
namespace Coditech.Admin.Utilities
{
    public class EncryptedUrlHelper : UrlHelper
    {
        public EncryptedUrlHelper(ActionContext actionContext) : base(actionContext) { }

        public override string Action(UrlActionContext actionContext)
        {
            var url = base.Action(actionContext);

            if (string.IsNullOrEmpty(url))
                return url;

            // If encryption Is disable , then return plain text
            if (!CoditechAdminSettings.IsURLEncrypted)
                return url;

            var parts = url.Split('?');
            if (parts.Length > 1)
            {
                var query = parts[1];
                var encrypted = EncryptionHelper.Encrypt(query);
                url = parts[0] + "?data=" + Uri.EscapeDataString(encrypted);
            }

            return url;
        }

    }

    public class EncryptedUrlHelperFactory : IUrlHelperFactory
    {
        public IUrlHelper GetUrlHelper(ActionContext context)
        {
            return new EncryptedUrlHelper(context);
        }
    }
}
