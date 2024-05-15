using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalDoctorVisitingChargesEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorVisitingCharges/GetHospitalDoctorVisitingChargesList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateHospitalDoctorVisitingChargesAsync() =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorVisitingCharges/CreateHospitalDoctorVisitingCharges";

        public string GetHospitalDoctorVisitingChargesAsync(short hospitalDoctorVisitingChargesId) =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorVisitingCharges/GetHospitalDoctorVisitingCharges?hospitalDoctorVisitingChargesId={hospitalDoctorVisitingChargesId}";
       
        public string UpdateHospitalDoctorVisitingChargesAsync() =>
               $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorVisitingCharges/UpdateHospitalDoctorVisitingCharges";

        public string DeleteHospitalDoctorVisitingChargesAsync() =>
                  $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalDoctorVisitingCharges/DeleteHospitalDoctorVisitingChargesy";
    }
}
