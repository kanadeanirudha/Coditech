using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalPathologyTestEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPathologyTest/GetHospitalPathologyTestList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateHospitalPathologyTestAsync() =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPathologyTest/CreateHospitalPathologyTest";
        public string GetHospitalPathologyTestAsync(long hospitalPathologyTestId) =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPathologyTest/GetHospitalPathologyTest?hospitalPathologyTestId={hospitalPathologyTestId}";

        public string UpdateHospitalPathologyTestAsync() =>
               $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPathologyTest/UpdateHospitalPathologyTest";

        public string DeleteHospitalPathologyTestAsync() =>
                  $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPathologyTest/DeleteHospitalPathologyTest";
    }
}
