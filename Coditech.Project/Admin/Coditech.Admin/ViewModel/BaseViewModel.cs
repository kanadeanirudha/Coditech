using Coditech.Admin.Utilities;
using Coditech.Common.Helper;
using Microsoft.Net.Http.Headers;
using System.Net;

namespace Coditech.Admin.ViewModel
{
    public abstract class BaseViewModel
    {
        public BaseViewModel()
        {
            PageListViewModel = new PageListViewModel();
        }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }

        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public PageListViewModel PageListViewModel { get; set; }
        public string ReturnUrl
        {
            get
            {

                string returnUrl = string.Empty;
                if (!string.Equals(HttpContextHelper.Current.Request.Query["returnUrl"].ToString(), Convert.ToString(HttpContextHelper.Request.Headers[HeaderNames.Referer]), StringComparison.OrdinalIgnoreCase))
                {
                    SessionHelper.SaveDataInSession<string>("returnUrl", Convert.ToString(HttpContextHelper.Request.Headers[HeaderNames.Referer]));
                }
                return WebUtility.UrlEncode(SessionHelper.GetDataFromSession<string>("returnUrl") ?? HttpContextHelper.Request.Headers[HeaderNames.Referer].ToString());

            }
        }
    }
}
