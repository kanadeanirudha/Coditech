using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalPatientAppointmentPurposeEndPoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCountryMaster/GetCountryList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateHospitalPatientAppointmentPurposeAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCountryMaster/CreateCountry";

        public string GetHospitalPatientAppointmentPurposeAsync(short HospitalPatientAppointmentPurposeId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCountryMaster/GetCountry?generalCountryMasterId={HospitalPatientAppointmentPurposeId}";

        public string UpdateHospitalPatientAppointmentPurposeAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCountryMaster/UpdateCountry";

        public string DeleteHospitalPatientAppointmentPurposeAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCountryMaster/DeleteCountry";
    }
}
