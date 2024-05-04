using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalDoctorAllocatedOPDRoomEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, short selectedDepartmentId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorAllocatedOPDRoom/GetHospitalDoctorAllocatedOPDRoomList?selectedCentreCode={selectedCentreCode}&selectedDepartmentId={selectedDepartmentId}{BuildEndpointQueryString(true,expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateHospitalDoctorAllocatedOPDRoomAsync() =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorAllocatedOPDRoom/CreateHospitalDoctorAllocatedOPDRoom";

        public string GetHospitalDoctorAllocatedOPDRoomAsync(int hospitalDoctorId, int hospitalDoctorAllocatedOPDRoomId) =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorAllocatedOPDRoom/GetHospitalDoctorAllocatedOPDRoom?hospitalDoctorId={hospitalDoctorId}&hospitalDoctorAllocatedOPDRoomId={hospitalDoctorAllocatedOPDRoomId}";
       
        public string UpdateHospitalDoctorAllocatedOPDRoomAsync() =>
               $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorAllocatedOPDRoom/UpdateHospitalDoctorAllocatedOPDRoom";

        public string DeleteHospitalDoctorAllocatedOPDRoomAsync() =>
                  $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorAllocatedOPDRoom/DeleteHospitalDoctorAllocatedOPDRoom";
    }
}
