using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralCountryEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCountryMaster/GetCountryList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateCountryAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCountryMaster/CreateCountry";

        public string GetCountryAsync(short generalCountryId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCountryMaster/GetCountry?generalCountryMasterId={generalCountryId}";
       
        public string UpdateCountryAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCountryMaster/UpdateCountry";

        public string DeleteCountryAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCountryMaster/DeleteCountry";
    }
}
