using System.Data;
using AspNetCore.Reporting;
using Coditech.Admin.Agents;
using Coditech.Admin.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class AccountReportController : BaseController
    {
        private IWebHostEnvironment Environment;
        private readonly IAccountReportAgent _accountBalanceSheetReportAgent;

        public AccountReportController(IWebHostEnvironment _environment, IAccountReportAgent accountBalanceSheetReportAgent)
        {
            this.Environment = _environment;
            _accountBalanceSheetReportAgent = accountBalanceSheetReportAgent;
        }
        #region BalanceSheetReport 
        [HttpGet]
        public virtual IActionResult AccountBalanceSheetReport()
        {
            AccountBalanceSheetReportViewModel model = new AccountBalanceSheetReportViewModel();
            model.FromDate = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToShortDateString());
            model.ToDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            return View("~/Views/Accounts/Reports/AccountBalanceSheetReport.cshtml", model);
        }

        [HttpPost]
        public IActionResult AccountBalanceSheetReport(AccountBalanceSheetReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                DataTableViewModel dataTableModel = new DataTableViewModel()
                {
                    SelectedCentreCode = model.SelectedCentreCode,
                    SelectedParameter1 = model.AccSetupBalanceSheetId.ToString(),
                    SelectedParameter2 = model.GeneralFinancialYearId.ToString(),
                    PageSize = int.MaxValue
                };
                AccountBalanceSheetReportListViewModel list = _accountBalanceSheetReportAgent.GetBalanceSheetReportList(dataTableModel);
                if (list?.AccountBalanceSheetReportList?.Count > 0)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Clear();
                    dataTable.Columns.Add("AccSetupBalanceSheetId", typeof(string));
                    dataTable.Columns.Add("AccSetupGLId", typeof(string));
                    dataTable.Columns.Add("GLName", typeof(string));
                    dataTable.Columns.Add("IsGroup", typeof(bool));
                    dataTable.Columns.Add("CategoryName", typeof(string));
                    dataTable.Columns.Add("ClosingBalance", typeof(string));
                    dataTable.Columns.Add("GeneralFinancialYearId", typeof(string));

                    foreach (var item in list.AccountBalanceSheetReportList)
                    {
                        dataTable.Rows.Add(item.AccSetupBalanceSheetId, item.AccSetupGLId, item.GLName, item.IsGroup, item.CategoryName, item.ClosingBalance, item.GeneralFinancialYearId);
                    }

                    Dictionary<string, string> reportParameters = new Dictionary<string, string>();

                    string reportName = "AccountGLBalanceSheetReport";

                    Stream stream = null;

                    stream = GetReport(this.Environment, "Accounts", "AccountGLBalanceSheet", dataTable, "DataSet1", reportParameters, RenderType.Pdf);
                    return File(stream, "application/pdf", $"{reportName}.pdf");
                }
                else
                {
                    model.IsRecordFound = false;
                }
            }
            return View("~/Views/Accounts/Reports/AccountBalanceSheetReport.cshtml", model);
        }
        #endregion
        #region Profit And Loss 
        [HttpGet]
        public virtual IActionResult AccountProfitAndLossReport()
        {
            AccountProfitAndLossReportViewModel model = new AccountProfitAndLossReportViewModel();
            model.FromDate = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToShortDateString());
            model.ToDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            return View("~/Views/Accounts/Reports/AccountProfitAndLossReport.cshtml", model);
        }

        [HttpPost]
        public IActionResult AccountProfitAndLossReport(AccountProfitAndLossReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                DataTableViewModel dataTableModel = new DataTableViewModel()
                {
                    SelectedCentreCode = model.SelectedCentreCode,
                    SelectedParameter1 = model.AccSetupBalanceSheetId.ToString(),
                    SelectedParameter2 = model.GeneralFinancialYearId.ToString(),
                    PageSize = int.MaxValue
                };
                AccountProfitAndLossReportListViewModel list = _accountBalanceSheetReportAgent.GetProfitAndLossReportList(dataTableModel);
                if (list?.AccountProfitAndLossReportList?.Count > 0)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Clear();
                    dataTable.Columns.Add("AccSetupBalanceSheetId", typeof(string));
                    dataTable.Columns.Add("AccSetupGLId", typeof(string));
                    dataTable.Columns.Add("GLName", typeof(string));
                    dataTable.Columns.Add("IsGroup", typeof(bool));
                    dataTable.Columns.Add("CategoryName", typeof(string));
                    dataTable.Columns.Add("ClosingBalance", typeof(string));
                    dataTable.Columns.Add("GeneralFinancialYearId", typeof(string));

                    foreach (var item in list.AccountProfitAndLossReportList)
                    {
                        dataTable.Rows.Add(item.AccSetupBalanceSheetId, item.AccSetupGLId, item.GLName, item.IsGroup, item.CategoryName, item.ClosingBalance, item.GeneralFinancialYearId);
                    }

                    Dictionary<string, string> reportParameters = new Dictionary<string, string>();

                    string reportName = "AccountGLProfitAndLossReport";

                    Stream stream = null;

                    stream = GetReport(this.Environment, "Accounts", "AccountGLProfitAndLossReport", dataTable, "DataSet1", reportParameters, RenderType.Pdf);
                    return File(stream, "application/pdf", $"{reportName}.pdf");
                }
                else
                {
                    model.IsRecordFound = false;
                }
            }
            return View("~/Views/Accounts/Reports/AccountBalanceSheetReport.cshtml", model);
        }
        #endregion
    }
}