using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class DBTMDeviceRegistrationDetailsEndpoint : BaseEndpoint
    {
        public string ListAsync(long UserMasterId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMDeviceRegistrationDetails/GetDBTMDeviceRegistrationDetailsList?UserMasterId={UserMasterId}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateRegistrationDetailsAsync() =>
            $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMDeviceRegistrationDetails/CreateRegistrationDetails";

        public string GetRegistrationDetailsAsync(long dBTMDeviceRegistrationDetailId) =>
            $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMDeviceRegistrationDetails/GetRegistrationDetails?dBTMDeviceRegistrationDetailId={dBTMDeviceRegistrationDetailId}";

        public string UpdateRegistrationDetailsAsync() =>
               $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMDeviceRegistrationDetails/UpdateRegistrationDetails";

        public string DeleteRegistrationDetailsAsync() =>
                  $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMDeviceRegistrationDetails/DeleteRegistrationDetails";
    }
}
