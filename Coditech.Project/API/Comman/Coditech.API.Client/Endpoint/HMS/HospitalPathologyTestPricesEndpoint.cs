using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.API.Data;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalPathologyTestPricesEndpoint : BaseEndpoint
    {
        public string ListAsync(int hospitalPathologyPriceCategoryEnumId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPathologyTestPrices/GetHospitalPathologyTestPricesList?hospitalPathologyPriceCategoryEnumId={hospitalPathologyPriceCategoryEnumId}{BuildEndpointQueryString(true,expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateHospitalPathologyTestPricesAsync() =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPathologyTestPrices/CreateHospitalPathologyTestPrices";
        public string GetHospitalPathologyTestPricesAsync(long hospitalPathologyTestPricesId) =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPathologyTestPrices/GetHospitalPathologyTestPrices?hospitalPathologyTestPricesId={hospitalPathologyTestPricesId}";

        public string UpdateHospitalPathologyTestPricesAsync() =>
               $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPathologyTestPrices/UpdateHospitalPathologyTestPrices";

        public string DeleteHospitalPathologyTestPricesAsync() =>
                  $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPathologyTestPrices/DeleteHospitalPathologyTestPrices";
    }
}
