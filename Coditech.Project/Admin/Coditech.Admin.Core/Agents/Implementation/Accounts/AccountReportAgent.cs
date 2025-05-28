using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

namespace Coditech.Admin.Agents
{
    public class AccountReportAgent : BaseAgent, IAccountReportAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAccountReportClient _accountReportClient;
        #endregion

        #region Public Constructor
        public AccountReportAgent(ICoditechLogging coditechLogging, IAccountReportClient accountReportClient)
        {
            _coditechLogging = coditechLogging;
            _accountReportClient = GetClient<IAccountReportClient>(accountReportClient);
        }
        #endregion

        #region Public Methods
        public virtual AccountBalanceSheetReportListViewModel GetBalanceSheetReportList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("GLName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);
            AccountBalanceSheetReportListResponse response = _accountReportClient.GetBalanceSheetReportList(dataTableModel.SelectedCentreCode, dataTableModel.SelectedParameter1, dataTableModel.SelectedParameter2, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            AccountBalanceSheetReportListModel accountBalanceSheetReportList = new AccountBalanceSheetReportListModel { AccountBalanceSheetReportList = response?.AccountBalanceSheetReportList };
            AccountBalanceSheetReportListViewModel listViewModel = new AccountBalanceSheetReportListViewModel();
            listViewModel.AccountBalanceSheetReportList = accountBalanceSheetReportList?.AccountBalanceSheetReportList?.ToViewModel<AccountBalanceSheetReportViewModel>().ToList();
            listViewModel.SelectedParameter1 = dataTableModel.SelectedParameter1;
            listViewModel.SelectedParameter2 = dataTableModel.SelectedParameter2;
            listViewModel.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.AccountBalanceSheetReportList.Count, BindColumns());
            return listViewModel;
        }

        #endregion
        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Invoice Number",
                ColumnCode = "AccSetupBalanceSheetId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Gym Member",
                ColumnCode = "CategoryName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "GL Accounts",
                ColumnCode = "GLName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Plan Type",
                ColumnCode = "GeneralFinancialYearId",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
