using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class DBTMTestEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTestMaster/GetDBTMTestList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateDBTMTestAsync() =>
            $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTestMaster/CreateDBTMTest";

        public string GetDBTMTestAsync(int dBTMTestMasterId) =>
            $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTestMaster/GetDBTMTest?dBTMTestMasterId={dBTMTestMasterId}";

        public string UpdateDBTMTestAsync() =>
               $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTestMaster/UpdateDBTMTest";

        public string DeleteDBTMTestAsync() =>
                  $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTestMaster/DeleteDBTMTest";

        public string GetDBTMTestParameterAsync() =>
           $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTestMaster/GetDBTMTestParameter";
    }
}
