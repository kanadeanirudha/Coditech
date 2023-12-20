using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralSystemGlobleSettingEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralSystemGlobleSetting/GetSystemGlobleSettingList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateSystemGlobleSettingAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralSystemGlobleSetting/CreateSystemGlobleSetting";

        public string GetSystemGlobleSettingAsync(short generalSystemGlobleSettingId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralSystemGlobleSetting/GetSystemGlobleSetting?generalSystemGlobleSettingId={generalSystemGlobleSettingId}";
       
        public string UpdateSystemGlobleSettingAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralSystemGlobleSetting/UpdateSystemGlobleSetting";

        public string DeleteSystemGlobleSettingAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralSystemGlobleSetting/DeleteSystemGlobleSetting";
    }
}
