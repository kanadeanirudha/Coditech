﻿using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.Helper;

using Microsoft.Net.Http.Headers;

using System.Security.Cryptography;
using System.Text;

namespace Coditech.Admin.Agents
{
    public abstract class BaseAgent
    {
        /// <summary>
        /// Get API client object with current domain name and key.
        /// </summary>
        /// <typeparam name="T">The type of API client object.</typeparam>
        /// <returns>An API client object of type T.</returns>
        protected T GetClient<T>(T obj) where T : IBaseClient
        {
            if (!(obj is BaseClient)) return obj;

            (obj as BaseClient).DomainName = System.Configuration.ConfigurationManager.AppSettings["ZnodeApiDomainName"];
            (obj as BaseClient).DomainKey = System.Configuration.ConfigurationManager.AppSettings["ZnodeApiDomainKey"];

            if (HttpContextHelper.Current?.User != null && HttpContextHelper.Current.User.Identity.IsAuthenticated)
            {
                //var model = SessionProxyHelper.GetUserDetails();
                //if (HelperUtility.IsNotNull(model))
                //{
                //    (obj as BaseClient).UserId = model.UserId;
                //    (obj as BaseClient).RefreshCache = true;
                //}
            }
            return obj;
        }

        ///// <summary>
        ///// Get API client object with current domain name and key.
        ///// </summary>
        ///// <typeparam name="T">The type of API client object.</typeparam>
        ///// <returns>An API client object of type T.</returns>
        ///// <summary>
        ///// Get API client object with current domain name and key.
        ///// </summary>
        ///// <typeparam name="T">The type of API client object.</typeparam>
        ///// <returns>An API client object of type T.</returns>
        //protected T GetClient<T>() where T : class
        //{
        //    var obj = Activator.CreateInstance<T>();

        //    if (!(obj is BaseClient)) return obj;
          
        //    (obj as BaseClient).DomainName = System.Configuration.ConfigurationManager.AppSettings["ApiDomainName"];
        //    (obj as BaseClient).DomainKey = System.Configuration.ConfigurationManager.AppSettings["ApiDomainKey"];

        //    if (HttpContextHelper.Current.User != null && HttpContextHelper.Current.User.Identity.IsAuthenticated)
        //    {
        //        //var model = SessionProxyHelper.GetUserDetails();
        //        //if (HelperUtility.IsNotNull(model))
        //        //{
        //        //    (obj as BaseClient).UserId = model.UserId;
        //        //    (obj as BaseClient).RefreshCache = true;
        //        //}
        //    }

        //    return obj;
        //}


        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
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
