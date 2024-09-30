using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class DBTMDeviceEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMDeviceMaster/GetDBTMDeviceList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateDBTMDeviceAsync() =>
            $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMDeviceMaster/CreateDBTMDevice";

        public string GetDBTMDeviceAsync(long dBTMDeviceId) =>
            $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMDeviceMaster/GetDBTMDevice?dBTMDeviceId={dBTMDeviceId}";

        public string UpdateDBTMDeviceAsync() =>
               $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMDeviceMaster/UpdateDBTMDevice";

        public string DeleteDBTMDeviceAsync() =>
                  $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMDeviceMaster/DeleteDBTMDevice";
    }
}
