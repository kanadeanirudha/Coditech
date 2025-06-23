
using System.Collections.Specialized;
using System.Data;
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Microsoft.Extensions.DependencyInjection;
namespace Coditech.API.Service
{
    public class AccountReportService : BaseService, IAccountReportService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralFinancialYear> _generalFinancialYearMasterRepository;

        public AccountReportService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalFinancialYearMasterRepository = new CoditechRepository<GeneralFinancialYear>(_serviceProvider.GetService<Coditech_Entities>());

        }
        #region BalanceSheet
        public virtual AccountBalanceSheetReportListModel GetBalanceSheetReportList(string SelectedCentreCode, string selectedParameter1, string selectedParameter2, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            // Bind the Filter, sorts & Paging shipPlan.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AccountBalanceSheetReportModel> objStoredProc = new CoditechViewRepository<AccountBalanceSheetReportModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@AccSetupBalanceSheetId", selectedParameter1, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@GeneralFinancialYearId", selectedParameter2, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AccountBalanceSheetReportModel> accountBalanceSheetList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAccGLBalanceSheetReport @AccSetupBalanceSheetId,@GeneralFinancialYearId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();

            AccountBalanceSheetReportListModel listModel = new AccountBalanceSheetReportListModel();
            listModel.AccountBalanceSheetReportList = accountBalanceSheetList?.Count > 0 ? accountBalanceSheetList : new List<AccountBalanceSheetReportModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        #endregion
        #region Profit And Loss
        public virtual AccountProfitAndLossReportListModel GetProfitAndLossReportList(string SelectedCentreCode, string selectedParameter1, string selectedParameter2, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            // Bind the Filter, sorts & Paging shipPlan.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AccountProfitAndLossReportModel> objStoredProc = new CoditechViewRepository<AccountProfitAndLossReportModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@AccSetupBalanceSheetId", selectedParameter1, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@GeneralFinancialYearId", selectedParameter2, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AccountProfitAndLossReportModel> accountProfitAndLossReportList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAccGLProfitAndLossReport @AccSetupBalanceSheetId,@GeneralFinancialYearId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 6, out pageListModel.TotalRowCount)?.ToList();

            AccountProfitAndLossReportListModel listModel = new AccountProfitAndLossReportListModel();
            listModel.AccountProfitAndLossReportList = accountProfitAndLossReportList?.Count > 0 ? accountProfitAndLossReportList : new List<AccountProfitAndLossReportModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        #endregion

    }
}
