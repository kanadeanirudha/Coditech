﻿using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalDoctorVisitingChargesEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, short selectedDepartmentId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorVisitingCharges/GetHospitalDoctorVisitingChargesList?selectedCentreCode={selectedCentreCode}&selectedDepartmentId={selectedDepartmentId}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string GetHospitalDoctorVisitingChargesByDoctorIdListAsync(int hospitalDoctorId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorVisitingCharges/GetHospitalDoctorVisitingChargesByDoctorIdList?HospitalDoctorId={hospitalDoctorId}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateHospitalDoctorVisitingChargesAsync() =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorVisitingCharges/CreateHospitalDoctorVisitingCharges";

        public string GetHospitalDoctorVisitingChargesAsync(long hospitalDoctorVisitingChargesId, int hospitalDoctorId) =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorVisitingCharges/GetHospitalDoctorVisitingCharges?hospitalDoctorVisitingChargesId={hospitalDoctorVisitingChargesId}&hospitalDoctorId={hospitalDoctorId}";
       
        public string UpdateHospitalDoctorVisitingChargesAsync() =>
               $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorVisitingCharges/UpdateHospitalDoctorVisitingCharges";

        public string DeleteHospitalDoctorVisitingChargesAsync() =>
                  $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorVisitingCharges/DeleteHospitalDoctorVisitingCharges";
    }
}
