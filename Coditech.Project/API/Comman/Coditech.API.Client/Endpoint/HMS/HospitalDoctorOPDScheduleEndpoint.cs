using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalDoctorOPDScheduleEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, short selectedDepartmentId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorOPDSchedule/GetHospitalDoctorOPDScheduleList?selectedCentreCode={selectedCentreCode}&selectedDepartmentId={selectedDepartmentId}{BuildEndpointQueryString(true,expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateHospitalDoctorOPDScheduleAsync() =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorOPDSchedule/CreateHospitalDoctorOPDSchedule";

        public string GetHospitalDoctorOPDScheduleAsync(int hospitalDoctorId, int weekDayEnumId) =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorOPDSchedule/GetHospitalDoctorOPDSchedule?hospitalDoctorId={hospitalDoctorId}&weekDayEnumId={weekDayEnumId}";
       
        public string UpdateHospitalDoctorOPDScheduleAsync() =>
               $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorOPDSchedule/UpdateHospitalDoctorOPDSchedule";

        public string DeleteHospitalDoctorOPDScheduleAsync() =>
                  $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorOPDSchedule/DeleteHospitalDoctorOPDSchedule";
    }
}
