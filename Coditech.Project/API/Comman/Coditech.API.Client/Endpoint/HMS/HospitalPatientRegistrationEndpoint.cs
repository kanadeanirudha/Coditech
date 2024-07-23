using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalPatientRegistrationEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientRegistration/GetPatientRegistrationList?selectedCentreCode={selectedCentreCode}{BuildEndpointQueryString(true,expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string GetPatientRegistrationOtherDetailAsync(long hospitalPatientRegistrationId) =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientRegistration/GetPatientRegistrationOtherDetail?hospitalPatientRegistrationId={hospitalPatientRegistrationId}";

        public string DeletePatientRegistrationAsync() =>
                  $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientRegistration/DeleteHospitalPatientRegistration";
    }
}
