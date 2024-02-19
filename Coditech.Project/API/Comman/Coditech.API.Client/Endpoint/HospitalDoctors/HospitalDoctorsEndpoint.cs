using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalDoctorsEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, short selectedDepartmentId, bool isAssociated,IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctors/GetHospitalDoctorsList?selectedCentreCode={selectedCentreCode}&selectedDepartmentId={selectedDepartmentId}&isAssociated={isAssociated}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateHospitalDoctorsAsync() =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctors/CreateHospitalDoctors";
        public string GetHospitalDoctorsAsync(long hospitalDoctorId) =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctors/GetHospitalDoctors?hospitalDoctorId={hospitalDoctorId}";

        public string UpdateHospitalDoctorsAsync() =>
               $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctors/UpdateHospitalDoctors";

        public string DeleteHospitalDoctorsAsync() =>
                  $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctors/DeleteHospitalDoctors";
    }
}
