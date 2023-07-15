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
    public class GeneralDepartmentAgent : BaseAgent, IGeneralDepartmentAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralDepartmentClient _generalDepartmentClient;
        #endregion

        #region Public Constructor
        public GeneralDepartmentAgent(ICoditechLogging coditechLogging, IGeneralDepartmentClient generalDepartmentClient)
        {
            _coditechLogging = coditechLogging;
            _generalDepartmentClient = GetClient<IGeneralDepartmentClient>(generalDepartmentClient);
        }
        #endregion
        #region Public Methods

        public GeneralDepartmentListViewModel GetDepartmentList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("DepartmentName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ContryCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn, dataTableModel.SortBy);
            GeneralDepartmentListResponse response = _generalDepartmentClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralDepartmentListModel departmentList = new GeneralDepartmentListModel { GeneralDepartmentList = response?.GeneralDepartmentList };
            GeneralDepartmentListViewModel listViewModel = new GeneralDepartmentListViewModel();
            listViewModel.GeneralDepartmentList = departmentList?.GeneralDepartmentList?.ToViewModel<GeneralDepartmentViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, departmentList, dataTableModel, listViewModel.GeneralDepartmentList.Count);

            return listViewModel;
        }
        #endregion
    }
}
