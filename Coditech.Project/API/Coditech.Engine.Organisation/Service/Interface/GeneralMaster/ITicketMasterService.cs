using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface ITicketMasterService
    {
        TicketMasterListModel GetTicketMasterList(long userMasterId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        TicketMasterModel CreateTicket(TicketMasterModel model);
        TicketMasterModel GetTicket(long ticketMasterId, long userMasterId);
        bool UpdateTicket(TicketMasterModel model);
        bool DeleteTicket(ParameterModel parameterModel);
    }
}
