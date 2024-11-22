using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class TaskMasterEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskMaster/GetTaskMasterList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateTaskMasterAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskMaster/CreateTaskMaster";

        public string GetTaskMasterAsync(short taskMasterId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskMaster/GetTaskMaster?taskMasterId={taskMasterId}";

        public string UpdateTaskMasterAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskMaster/UpdateTaskMaster";

        public string DeleteTaskMasterAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskMaster/DeleteTaskMaster";
    }
}
