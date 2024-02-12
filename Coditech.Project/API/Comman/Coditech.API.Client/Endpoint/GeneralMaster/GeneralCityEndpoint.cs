using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralCityEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCityMaster/GetCityList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateCityAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCityMaster/CreateCity";

        public string GetCityAsync(int cityId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCityMaster/GetCity?generalCityMasterId={cityId}";

        public string UpdateCityAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCityMaster/UpdateCity";

        public string DeleteCityAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCityMaster/DeleteCity";

        public string GetCityByRegionWise(Int16 generalRegionMasterId)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCityMaster/GetCityByRegionWise?generalRegionMasterId={generalRegionMasterId}";
            return endpoint;
        }
    }
}
