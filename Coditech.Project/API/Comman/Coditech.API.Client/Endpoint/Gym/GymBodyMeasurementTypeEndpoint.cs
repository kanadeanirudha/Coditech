using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GymBodyMeasurementTypeEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymBodyMeasurementType/GetGymBodyMeasurementTypeList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateGymBodyMeasurementTypeAsync() =>
            $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymBodyMeasurementType/CreateGymBodyMeasurementType";

        public string GetGymBodyMeasurementTypeAsync(short gymBodyMeasurementTypeId) =>
            $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymBodyMeasurementType/GetGymBodyMeasurementType?gymBodyMeasurementTypeId={gymBodyMeasurementTypeId}";
       
        public string UpdateGymBodyMeasurementTypeAsync() =>
               $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymBodyMeasurementType/UpdateGymBodyMeasurementType";

        public string DeleteGymBodyMeasurementTypeAsync() =>
                  $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/GymBodyMeasurementType/DeleteGymBodyMeasurementType";
    }
}
