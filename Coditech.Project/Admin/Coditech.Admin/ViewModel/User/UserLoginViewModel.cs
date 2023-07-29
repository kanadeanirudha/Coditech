using Coditech.Admin.Utilities;
using Coditech.Common.Helper;

using Microsoft.Net.Http.Headers;

using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Coditech.Admin.ViewModel
{
    public class UserLoginViewModel : BaseViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl
        {
            get
            {
                if (!string.Equals(HttpContextHelper.Current.Request.Query["returnUrl"].ToString(), Convert.ToString(HttpContextHelper.Request.Headers[HeaderNames.Referer]), StringComparison.OrdinalIgnoreCase))
                {
                    SessionHelper.SaveDataInSession<string>("returnUrl", Convert.ToString(HttpContextHelper.Request.Headers[HeaderNames.Referer]));
                }
                return WebUtility.UrlEncode(SessionHelper.GetDataFromSession<string>("returnUrl") ?? HttpContextHelper.Request.Headers[HeaderNames.Referer].ToString());

            }
        }
    }
}