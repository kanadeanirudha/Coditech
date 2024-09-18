using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralNotificationEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralNotificationMaster/GetNotification" +
                $"List{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateNotificationAsync() =>
     $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralNotificationMaster/CreateNotification";

        public string GetNotificationAsync(long generalNotificationId) =>
          $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralNotificationMaster/GetNotification?generalNotificationId={generalNotificationId}";

        public string UpdateNotificationAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralNotificationMaster/UpdateNotification";

        public string DeleteNotificationAsync() =>
                $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralNotificationMaster/DeleteNotification";

    }

}

