using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class GymSalesInvoiceAgent : BaseAgent, IGymSalesInvoiceAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGymSalesInvoiceClient _gymSalesInvoiceClient;
        #endregion

        #region Public Constructor
        public GymSalesInvoiceAgent(ICoditechLogging coditechLogging, IGymSalesInvoiceClient gymSalesInvoiceClient)
        {
            _coditechLogging = coditechLogging;
            _gymSalesInvoiceClient = GetClient<IGymSalesInvoiceClient>(gymSalesInvoiceClient);
        }
        #endregion

        #region Public Methods
        public virtual GymMemberSalesInvoiceListViewModel GymMemberServiceSalesInvoiceList(DateTime? toDate, DateTime? fromDate, DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("InvoiceNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GymMemberSalesInvoiceListResponse response = _gymSalesInvoiceClient.GymMemberServiceSalesInvoiceList(dataTableModel.SelectedCentreCode, toDate, fromDate, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GymMemberSalesInvoiceListModel gymMemberSalesInvoiceList = new GymMemberSalesInvoiceListModel { GymMemberSalesInvoiceList = response?.GymMemberSalesInvoiceList };
            GymMemberSalesInvoiceListViewModel listViewModel = new GymMemberSalesInvoiceListViewModel();
            listViewModel.GymMemberSalesInvoiceList = gymMemberSalesInvoiceList?.GymMemberSalesInvoiceList?.ToViewModel<GymMemberSalesInvoiceViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GymMemberSalesInvoiceList.Count, BindColumns());
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
                ColumnCode = "InvoiceNumber",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Gym Member",
                ColumnCode = "FirstName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Transaction Date",
                ColumnCode = "TransactionDate",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Plan Type",
                ColumnCode = "PlanType",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Plan Name",
                ColumnCode = "MembershipPlanName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Paid Amount",
                ColumnCode = "BillAmount",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Payment Mode",
                ColumnCode = "PaymentType",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Payment Received By",
                ColumnCode = "PaymentReceivedBy",
            });
            return datatableColumnList;
        }
        #endregion
    }
}
