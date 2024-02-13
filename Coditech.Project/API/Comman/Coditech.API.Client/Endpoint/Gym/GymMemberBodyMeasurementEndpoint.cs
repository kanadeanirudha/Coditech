using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GymMemberBodyMeasurementEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GymMemberBodyMeasurement/GetMemberBodyMeasurementList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateMemberBodyMeasurementAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GymMemberBodyMeasurement/CreateMemberBodyMeasurement";

        public string GetMemberBodyMeasurementAsync(long GymMemberBodyMeasurementId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GymMemberBodyMeasurement/GetMemberBodyMeasurement?GymMemberBodyMeasurementId={GymMemberBodyMeasurementId}";
       
        public string UpdateMemberBodyMeasurementAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GymMemberBodyMeasurement/UpdateMemberBodyMeasurement";

        public string DeleteMemberBodyMeasurementAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GymMemberBodyMeasurement/DeleteMemberBodyMeasurement";
    }
}
