using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{

    public class GeneralSmsProviderEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralSmsProviderMaster/GetSmsProviderList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateSmsProviderAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralSmsProviderMaster/CreateSmsProvider";

        public string GetSmsProviderAsync(short generalSmsProviderId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralSmsProviderMaster/GetSmsProvider?generalSmsProviderId={generalSmsProviderId}";

        public string UpdateSmsProviderAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralSmsProviderMaster/UpdateSmsProvider";

        public string DeleteSmsProviderAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralSmsProviderMaster/DeleteSmsProvider";
    }
}
