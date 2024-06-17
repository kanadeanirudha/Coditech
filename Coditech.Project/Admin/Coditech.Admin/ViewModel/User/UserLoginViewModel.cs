using Coditech.Admin.Utilities;
using Coditech.Common.Helper;

using Microsoft.Net.Http.Headers;

using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Coditech.Admin.ViewModel
{
    public class UserLoginViewModel : BaseViewModel
    {
        [MaxLength(100)]
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [MaxLength(100)]
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool IsPasswordChange { get; set; } = true;
        public string ReturnUrl
        {
            get
            {
                if (HttpContextHelper.Current.Request.Query.Count > 0 && !string.Equals(HttpContextHelper.Current.Request.Query["returnUrl"].ToString(), Convert.ToString(HttpContextHelper.Request.Headers[HeaderNames.Referer]), StringComparison.OrdinalIgnoreCase))
                {
                    SessionHelper.SaveDataInSession<string>("returnUrl", Convert.ToString(HttpContextHelper.Request.Headers[HeaderNames.Referer]));
                }
                return WebUtility.UrlEncode(SessionHelper.GetDataFromSession<string>("returnUrl"));
            }
        }
    }
}