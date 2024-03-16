using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGymSalesInvoiceAgent
    {
        /// <summary>
        /// Get Gym Member Service Sales Invoice List
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GymMemberSalesInvoiceListViewModel</returns>
        GymMemberSalesInvoiceListViewModel GymMemberServiceSalesInvoiceList(DataTableViewModel dataTableModel);
    }
}
