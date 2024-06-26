﻿using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;

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
                UserModel model = SessionProxyHelper.GetUserDetails();
                if (HelperUtility.IsNotNull(model))
                {
                    (obj as BaseClient).UserMasterId = model.UserMasterId;
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
                UserModel model = SessionProxyHelper.GetUserDetails();
                if (HelperUtility.IsNotNull(model))
                {
                    (obj as BaseClient).UserMasterId = model.UserMasterId;
                    (obj as BaseClient).RefreshCache = true;
                }
            }
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

        protected static SortCollection SortingByCreatedDate(string sortBy = AdminConstants.DESCKey)
        {
            SortCollection sortlist = new SortCollection();
            sortlist.Add(SortKeys.CreatedDate, sortBy);
            return sortlist;
        }

        protected static SortCollection SortingByModifiedDate(string sortBy = AdminConstants.DESCKey)
        {
            SortCollection sortlist = new SortCollection();
            sortlist.Add(SortKeys.ModifiedDate, sortBy);
            return sortlist;
        }

        protected static SortCollection SortingData(string sortByColumn, string sortBy)
        {
            if (!string.IsNullOrEmpty(sortByColumn))
            {
                SortCollection sortlist = new SortCollection();
                sortlist.Add(sortByColumn, sortBy);
                return sortlist;
            }
            return null;
        }
        protected void SetListPagingData(PageListViewModel pageListViewModel, BaseListResponse listModel, DataTableViewModel dataTableModel, int totalRecordCount, List<DatatableColumns> datatableColumns, bool IsActionColumn = true)
        {
            pageListViewModel.Page = Convert.ToInt32(listModel.PageIndex);
            pageListViewModel.RecordPerPage = Convert.ToInt32(listModel.PageSize);
            pageListViewModel.TotalPages = Convert.ToInt32(listModel.TotalPages);
            pageListViewModel.TotalResults = Convert.ToInt32(listModel.TotalResults);
            pageListViewModel.TotalRecordCount = Convert.ToInt32(totalRecordCount);
            pageListViewModel.SearchBy = dataTableModel.SearchBy ?? string.Empty;
            pageListViewModel.SortByColumn = dataTableModel.SortByColumn ?? string.Empty;
            pageListViewModel.SortBy = dataTableModel.SortBy ?? string.Empty;
            pageListViewModel.DatatableColumnList = datatableColumns;
            pageListViewModel.IsActionColumn = IsActionColumn;
        }

        protected string SpiltCentreCode(string centreCode)
        {
            centreCode = !string.IsNullOrEmpty(centreCode) && centreCode.Contains(":") ? centreCode.Split(':')[0] : centreCode;
            return centreCode;
        }
    }
}
