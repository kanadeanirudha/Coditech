using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using System.Collections.Specialized;
using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.Admin.Agents
{
    public class GeneralCountryAgent : BaseAgent, IGeneralCountryAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralCountryClient _generalCountryClient;
        #endregion

        #region Public Constructor
        public GeneralCountryAgent(ICoditechLogging coditechLogging, IGeneralCountryClient generalCountryClient)
        {
            _coditechLogging = coditechLogging;
            _generalCountryClient = GetClient<IGeneralCountryClient>(generalCountryClient);
        }
        #endregion

        #region Public Methods
        public GeneralCountryListViewModel GetCountryList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("CountryName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ContryCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            SortCollection sortlist = SortingData(dataTableModel.SortByColumn, dataTableModel.SortBy);
            GeneralCountryListResponse response = _generalCountryClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralCountryListModel countryList = new GeneralCountryListModel { GeneralCountryList = response?.GeneralCountryList };
            GeneralCountryListViewModel listViewModel = new GeneralCountryListViewModel();
            listViewModel.GeneralCountryList = countryList?.GeneralCountryList?.ToViewModel<GeneralCountryViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, countryList, dataTableModel, listViewModel.GeneralCountryList.Count);
            return listViewModel;
        }
        #endregion
    }
}
