using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class MediaSettingMasterEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/MediaSettingMaster/GetMediaSettingMasterList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateMediaSettingMasterAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/MediaSettingMaster/CreateMediaSettingMaster";

        public string GetMediaSettingMasterAsync(short mediaSettingMasterId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/MediaSettingMaster/GetMediaSettingMaster?mediaSettingMasterId={mediaSettingMasterId}";
       
        public string UpdateMediaSettingMasterAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/MediaSettingMaster/UpdateMediaSettingMaster";

        public string DeleteMediaSettingMasterAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/MediaSettingMaster/DeleteMediaSettingMaster";
    }
}
