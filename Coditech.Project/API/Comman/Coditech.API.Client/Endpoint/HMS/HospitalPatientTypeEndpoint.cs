using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalPatientTypeEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientType/GetHospitalPatientTypeList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateHospitalPatientTypeAsync() =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientType/CreateHospitalPatientType";
        public string GetHospitalPatientTypeAsync(byte hospitalPatientTypeId) =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientType/GetHospitalPatientType?hospitalPatientTypeId={hospitalPatientTypeId}";

        public string UpdateHospitalPatientTypeAsync() =>
               $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientType/UpdateHospitalPatientType";

        public string DeleteHospitalPatientTypeAsync() =>
                  $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientType/DeleteHospitalPatientType";
    }
}
