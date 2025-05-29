using Coditech.Admin.ViewModel;
namespace Coditech.Admin.Agents
{
    public interface IAccountReportAgent
    {
        /// <summary>
        /// Get Gym Member Service Sales Invoice List
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GymMemberSalesInvoiceListViewModel</returns>
        AccountBalanceSheetReportListViewModel GetBalanceSheetReportList(DataTableViewModel dataTableModel);
        /// <summary>
        /// Get Gym Member Service Sales Invoice List
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GymMemberSalesInvoiceListViewModel</returns>
        AccountProfitAndLossReportListViewModel GetProfitAndLossReportList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Get Sales Invoice Details by SalesInvoicePrintld.
        /// </summary>
        /// <param name="personId">salesInvoiceMasterId</param>
        /// <returns>Returns SalesInvoicePrintModel.</returns>
        //AccountBalanceSheetReportModel GetSalesInvoiceDetails(long Id);
    }
}
