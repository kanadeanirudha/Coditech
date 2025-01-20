using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class TaskApprovalSettingEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskApprovalSetting/GetTaskApprovalSettingList?selectedCentreCode={selectedCentreCode}{BuildEndpointQueryString(true,expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string GetTaskApprovalSettingAsync(short taskMasterId, string centreCode) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskApprovalSetting/GetTaskApprovalSetting?taskMasterId={taskMasterId}&centreCode={centreCode}";

        public string AddUpdateTaskApprovalSettingAsync() =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskApprovalSetting/AddUpdateTaskApprovalSetting";

        public string GetUpdateTaskApprovalSettingAsync(short taskMasterId, string centreCode, int taskApprovalSettingId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskApprovalSetting/GetUpdateTaskApprovalSetting?taskMasterId={taskMasterId}&centreCode={centreCode}&taskApprovalSettingId={taskApprovalSettingId}";
        public string UpdateTaskApprovalSettingAsync() =>
                $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskApprovalSetting/UpdateTaskApprovalSetting";

        public string DeleteTaskApprovalSettingAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskApprovalSetting/DeleteTaskApprovalSetting";
    }
}
