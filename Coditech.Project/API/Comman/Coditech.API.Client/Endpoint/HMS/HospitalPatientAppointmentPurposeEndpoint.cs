using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalPatientAppointmentPurposeEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientAppointmentPurpose/GetHospitalPatientAppointmentPurposeList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateHospitalPatientAppointmentPurposeAsync() =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientAppointmentPurpose/CreateHospitalPatientAppointmentPurpose";

        public string GetHospitalPatientAppointmentPurposeAsync(short hospitalPatientAppointmentPurposeId) =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientAppointmentPurpose/GetHospitalPatientAppointmentPurpose?hospitalPatientAppointmentPurposeId={hospitalPatientAppointmentPurposeId}";

        public string UpdateHospitalPatientAppointmentPurposeAsync() =>
               $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientAppointmentPurpose/UpdateHospitalPatientAppointmentPurpose";

        public string DeleteHospitalPatientAppointmentPurposeAsync() =>
                  $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientAppointmentPurpose/DeleteHospitalPatientAppointmentPurpose";
    }
}
