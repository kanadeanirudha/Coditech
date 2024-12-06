using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface ITicketMasterClient : IBaseClient
    {
        /// <summary>
        /// Get list of TicketMaster.
        /// </summary>
        /// <returns>TicketMasterListResponse</returns>
        TicketMasterListResponse List(long userMasterId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create TicketMaster.
        /// </summary>
        /// <param name="ticketMasterModel">ticketMasterModel.</param>
        /// <returns>Returns TicketMasterResponse.</returns>
        TicketMasterResponse CreateTicket(TicketMasterModel body);

        /// <summary>
        /// Get TicketMaster by ticketMasterId.
        /// </summary>
        /// <param name="ticketMasterId">ticketMasterId</param>
        /// <returns>Returns TicketMasterResponse.</returns>
        TicketMasterResponse GetTicket(long ticketMasterId);

        /// <summary>
        /// Update TicketMaster.
        /// </summary>
        /// <param name="TicketMasterModel">TicketMasterModel.</param>
        /// <returns>Returns updated TicketMasterResponse</returns>
        TicketMasterResponse UpdateTicket(TicketMasterModel model);

        /// <summary>
        /// Delete TicketMaster.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteTicket(ParameterModel body);
    }
}
