using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralMeasurementUnitEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralMeasurementUnitMaster/GetMeasurementUnitList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateMeasurementUnitAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralMeasurementUnitMaster/CreateMeasurementUnit";

        public string GetMeasurementUnitAsync(short generalMeasurementUnitId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralMeasurementUnitMaster/GetMeasurementUnit?generalMeasurementUnitMasterId={generalMeasurementUnitId}";
       
        public string UpdateMeasurementUnitAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralMeasurementUnitMaster/UpdateMeasurementUnit";

        public string DeleteMeasurementUnitAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralMeasurementUnitMaster/DeleteMeasurementUnit";
    }
}
