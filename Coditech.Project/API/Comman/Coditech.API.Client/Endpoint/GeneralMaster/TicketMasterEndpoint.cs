using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.API.Data;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class TicketMasterEndpoint : BaseEndpoint
    {
        public string ListAsync(long userMasterId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TicketMaster/GetTicketMasterList?userMasterId={userMasterId}{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateTicketAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TicketMaster/CreateTicket";

        public string GetTicketAsync(long ticketMasterId, long userMasterId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TicketMaster/GetTicket?ticketMasterId={ticketMasterId}&userMasterId={userMasterId}";

        public string UpdateTicketAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TicketMaster/UpdateTicket";

        public string DeleteTicketAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TicketMaster/DeleteTicket";
    }
}
