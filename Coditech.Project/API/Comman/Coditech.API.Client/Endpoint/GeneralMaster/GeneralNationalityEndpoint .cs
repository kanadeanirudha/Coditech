using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralNationalityEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralNationalityMaster/GetNationalityList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateNationalityAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralNationalityMaster/CreateNationality";

        public string GetNationalityAsync(short nationalityId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralNationalityMaster/GetNationality?generalNationalityMasterId={nationalityId}";

        public string UpdateNationalityAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralNationalityMaster/UpdateNationality";

        public string DeleteNationalityAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralNationalityMaster/DeleteNationality";
    }
}
