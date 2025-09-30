using Coditech.Admin.Utilities;
using Coditech.API.Data;
using Coditech.Common.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;

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
                    if (CoditechAdminSettings.IsURLEncrypted)
                    {
                        string url = string.Empty;
                        try
                        {
                            string returnUrl = HttpContextHelper.Current.Request.Query["ReturnUrl"];
                            if (returnUrl?.Split("?").Length > 1)
                            {
                                string encryptedString = HttpUtility.UrlEncode(EncryptionHelper.Encrypt(returnUrl.Split("?")[1]));
                                url = $"{returnUrl.Split("?")[0]}?data={encryptedString}";
                            }
                            else
                            {
                                url = returnUrl;
                            }
                            SessionHelper.SaveDataInSession<string>("returnUrl", url);
                        }
                        catch { }
                    }
                    else
                        SessionHelper.SaveDataInSession<string>("returnUrl", Convert.ToString(HttpContextHelper.Request.Headers[HeaderNames.Referer]));
                }
                return WebUtility.UrlEncode(SessionHelper.GetDataFromSession<string>("returnUrl"));
            }
        }
    }
}