﻿using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalPatientAppointmentEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, short selectedDepartmentId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientAppointment/GetHospitalPatientAppointmentList?selectedCentreCode={selectedCentreCode}&selectedDepartmentId={selectedDepartmentId}{BuildEndpointQueryString(true,expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateHospitalPatientAppointmentAsync() =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientAppointment/CreateHospitalPatientAppointment";

        public string GetHospitalPatientAppointmentAsync(long hospitalPatientAppointmentId) =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientAppointment/GetHospitalPatientAppointment?hospitalPatientAppointmentId={hospitalPatientAppointmentId}";
       
        public string UpdateHospitalPatientAppointmentAsync() =>
               $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientAppointment/UpdateHospitalPatientAppointment";

        public string DeleteHospitalPatientAppointmentAsync() =>
                  $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPatientAppointment/DeleteHospitalPatientAppointment";
    }
}