using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class CoditechApplicationSettingEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/CoditechApplicationSetting/GetCoditechApplicationSettingList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateCoditechApplicationSettingAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/CoditechApplicationSetting/CreateCoditechApplicationSetting";

        public string GetCoditechApplicationSettingAsync(short coditechApplicationSettingId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/CoditechApplicationSetting/GetCoditechApplicationSetting?coditechApplicationSettingId={coditechApplicationSettingId}";
       
        public string UpdateCoditechApplicationSettingAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/CoditechApplicationSetting/UpdateCoditechApplicationSetting";

        public string DeleteCoditechApplicationSettingAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/CoditechApplicationSetting/DeleteCoditechApplicationSetting";
    }
}
