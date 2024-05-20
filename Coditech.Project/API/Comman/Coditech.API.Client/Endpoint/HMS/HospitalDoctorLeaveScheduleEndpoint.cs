using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalDoctorLeaveScheduleEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, short selectedDepartmentId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorLeaveSchedule/GetHospitalDoctorLeaveScheduleList?selectedCentreCode={selectedCentreCode}&selectedDepartmentId={selectedDepartmentId}{BuildEndpointQueryString(true,expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateHospitalDoctorLeaveScheduleAsync() =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorLeaveSchedule/CreateHospitalDoctorLeaveSchedule";

        public string GetHospitalDoctorLeaveScheduleAsync(long hospitalDoctorLeaveScheduleId) =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorLeaveSchedule/GetHospitalDoctorLeaveSchedule?hospitalDoctorLeaveScheduleId={hospitalDoctorLeaveScheduleId}";
       
        public string UpdateHospitalDoctorLeaveScheduleAsync() =>
               $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorLeaveSchedule/UpdateHospitalDoctorLeaveSchedule";

        public string DeleteHospitalDoctorLeaveScheduleAsync() =>
                  $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorLeaveSchedule/DeleteHospitalDoctorLeaveSchedule";
    }
}
