using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class MediaSettingMasterEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaSettingMaster/GetMediaSettingMasterList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string GetMediaSettingMasterAsync(byte mediaTypeMasterId) =>
            $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaSettingMaster/GetMediaSettingMaster?mediaTypeMasterId={mediaTypeMasterId}";
       
        public string UpdateMediaSettingMasterAsync() =>
               $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaSettingMaster/UpdateMediaSettingMaster";

        public string DeleteMediaSettingMasterAsync() =>
                  $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaSettingMaster/DeleteMediaSettingMaster";
    }
}
