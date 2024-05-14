using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalDoctorVisitingChargesEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/HospitalDoctorVisitingChargesMaster/GetHospitalDoctorVisitingChargesList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateHospitalDoctorVisitingChargesAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/HospitalDoctorVisitingCharges/CreateHospitalDoctorVisitingCharges";

        public string GetHospitalDoctorVisitingChargesAsync(short hospitalDoctorVisitingChargesId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/HospitalDoctorVisitingCharges/GetHospitalDoctorVisitingCharges?hospitalDoctorVisitingChargesId={hospitalDoctorVisitingChargesId}";
       
        public string UpdateHospitalDoctorVisitingChargesAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/HospitalDoctorVisitingCharges/UpdateHospitalDoctorVisitingCharges";

        public string DeleteHospitalDoctorVisitingChargesAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/HospitalDoctorVisitingCharges/DeleteHospitalDoctorVisitingChargesy";
    }
}
