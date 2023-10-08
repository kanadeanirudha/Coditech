using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class AdminSanctionPostEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminSanctionPost/GetAdminSanctionPostList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateAdminSanctionPostAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminSanctionPost/CreateAdminSanctionPost";

        public string GetAdminSanctionPostAsync(int adminSanctionPostId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminSanctionPost/GetAdminSanctionPost?adminSanctionPostId={adminSanctionPostId}";
       
        public string UpdateAdminSanctionPostAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminSanctionPost/UpdateAdminSanctionPost";

        public string DeleteAdminSanctionPostAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminSanctionPost/DeleteAdminSanctionPost";
    }
}
