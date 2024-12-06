using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface ITicketMasterAgent
    {
        /// <summary>
        /// Get list of Ticket Master.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>TicketMasterListViewModel</returns>
        TicketMasterListViewModel GetTicketMasterList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Ticket Master.
        /// </summary>
        /// <param name="ticketMasterViewModel">Ticket Master View Model.</param>
        /// <returns>Returns created model.</returns>
        TicketMasterViewModel CreateTicket(TicketMasterViewModel ticketMasterViewModel);

        /// <summary>
        /// Get Ticket by ticketMasterId.
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>Returns TicketMasterViewModel.</returns>
        TicketMasterViewModel GetTicket(long userId);

        /// <summary>
        /// Update TicketMaster.
        /// </summary>
        /// <param name="ticketMasterViewModel">ticketMasterViewModel.</param>
        /// <returns>Returns updated TicketMasterViewModel</returns>
        TicketMasterViewModel UpdateTicket(TicketMasterViewModel ticketMasterViewModel);

        /// <summary>
        /// Delete TicketMaster.
        /// </summary>
        /// <param name="ticketMasterIds">ticketMasterIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteTicket(string ticketMasterIds, out string errorMessage);
    }
}
