using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.Helper;

namespace Coditech.Admin.Agents
{
    public abstract class BaseAgent
    {
        /// <summary>
        /// Get API client object with current domain name and key.
        /// </summary>
        /// <typeparam name="T">The type of API client object.</typeparam>
        /// <returns>An API client object of type T.</returns>
        protected T GetClient<T>() where T : class
        {
            var obj = Activator.CreateInstance<T>();

            if (!(obj is BaseClient)) return obj;

            (obj as BaseClient).DomainName = System.Configuration.ConfigurationManager.AppSettings["ApiDomainName"];
            (obj as BaseClient).DomainKey = System.Configuration.ConfigurationManager.AppSettings["ApiDomainKey"];

            if (HttpContextHelper.Current.User != null && HttpContextHelper.Current.User.Identity.IsAuthenticated)
            {
                UserViewModel model = SessionProxyHelper.GetUserDetails();
                if (HelperUtility.IsNotNull(model))
                {
                    (obj as BaseClient).UserId = model.UserId;
                    (obj as BaseClient).RefreshCache = true;
                }
            }

            return obj;
        }

        /// <summary>
        /// Get API client object with current domain name and key.
        /// </summary>
        /// <typeparam name="T">The type of API client object.</typeparam>
        /// <returns>An API client object of type T.</returns>
        protected T GetClient<T>(T obj) where T : IBaseClient
        {
            if (!(obj is BaseClient)) return obj;
            (obj as BaseClient).DomainName = System.Configuration.ConfigurationManager.AppSettings["ApiDomainName"];
            (obj as BaseClient).DomainKey = System.Configuration.ConfigurationManager.AppSettings["ApiDomainKey"];

            if (HttpContextHelper.Current.User != null && HttpContextHelper.Current.User.Identity.IsAuthenticated)
            {
                var model = SessionProxyHelper.GetUserDetails();
                if (HelperUtility.IsNotNull(model))
                {
                    (obj as BaseClient).UserId = model.UserId;
                    (obj as BaseClient).RefreshCache = true;
                }
            }
            //TODO Team Hornbills 
            //ICustomHeaders _headerAgent = GetService<ICustomHeaders>();

            //if (HelperUtility.IsNotNull(_headerAgent))
            //{
            //    Dictionary<string, string> headers = _headerAgent.SetCustomHeaderOfClient();

            //    int? count = headers?.Count;

            //    if (count > 0)
            //    {
            //        for (int i = 0; i < count; i++)
            //        {
            //            switch (i.ToString())
            //            {
            //                case "0":
            //                    (obj as BaseClient).Custom1 = $"{headers.ElementAt(i).Key}:{headers.ElementAt(i).Value}";
            //                    break;
            //                case "1":
            //                    (obj as BaseClient).Custom2 = $"{headers.ElementAt(i).Key}:{headers.ElementAt(i).Value}";
            //                    break;
            //                case "2":
            //                    (obj as BaseClient).Custom3 = $"{headers.ElementAt(i).Key}:{headers.ElementAt(i).Value}";
            //                    break;
            //                case "3":
            //                    (obj as BaseClient).Custom4 = $"{headers.ElementAt(i).Key}:{headers.ElementAt(i).Value}";
            //                    break;
            //                case "4":
            //                    (obj as BaseClient).Custom5 = $"{headers.ElementAt(i).Key}:{headers.ElementAt(i).Value}";
            //                    break;
            //            }
            //        }
            //    }
            //}
            return obj;
        }


        /// <summary>
        /// Gets an object from session.
        /// </summary>
        /// <typeparam name="T">The type of object to retrieve.</typeparam>
        /// <param name="key">The key for the session object being retrieved.</param>
        /// <returns>The object of type T from session.</returns>
        protected T GetFromSession<T>(string key)
        {
            return SessionHelper.GetDataFromSession<T>(key);
        }

        /// <summary>
        /// Saves an object in session.
        /// </summary>
        /// <typeparam name="T">The type of object being saved.</typeparam>
        /// <param name="key">The key for the session object.</param>
        /// <param name="value">The value of the session object.</param>
        protected void SaveInSession<T>(string key, T value)
        {
            SessionHelper.SaveDataInSession<T>(key, value);
        }

        /// <summary>
        /// Removes an object from session.
        /// </summary>
        /// <param name="key">The key of the session object.</param>
        protected void RemoveInSession(string key)
        {
            SessionHelper.RemoveDataFromSession(key);
        }
        /// <summary>
        /// Get BaseViewModel with HasError and ErrorMessage set.
        /// </summary>
        /// <param name="viewModel">View model to set.</param>
        /// <param name="errorMessage">Error message to set.</param>
        /// <returns>Returns BaseViewModel with HasError and ErrorMessage set.</returns>
        protected BaseViewModel GetViewModelWithErrorMessage(BaseViewModel viewModel, string errorMessage)
        {
            viewModel.HasError = true;
            viewModel.ErrorMessage = errorMessage;
            return viewModel;
        }
    }
}
