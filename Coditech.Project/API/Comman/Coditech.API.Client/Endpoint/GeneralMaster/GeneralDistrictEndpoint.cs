using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralDistrictEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDistrictMaster/GetDistrictList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateDistrictAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDistrictMaster/CreateDistrict";

        public string GetDistrictAsync(short generalDistrictId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDistrictMaster/GetDistrict?generalDistrictMasterId={generalDistrictId}";
       
        public string UpdateDistrictAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDistrictMaster/UpdateDistrict";

        public string DeleteDistrictAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDistrictMaster/DeleteDistrict";

        public string GetDistrictByRegionWise(Int16 generalRegionMasterId)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDistrictMaster/GetDistrictByRegionWise?generalRegionMasterId={generalRegionMasterId}";
            return endpoint;
        }
    }
}
