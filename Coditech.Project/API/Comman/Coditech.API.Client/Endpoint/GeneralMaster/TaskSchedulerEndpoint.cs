using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.API.Data;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class TaskSchedulerEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskScheduler/GetTaskSchedulerList{BuildEndpointQueryString(expand)}";
            return endpoint;
        }

        public string CreateTaskSchedulerAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskScheduler/CreateTaskScheduler";

        public string GetTaskSchedulerDetailsAsync(int configuratorId, string schedulerCallFor) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskScheduler/GetTaskSchedulerDetails?configuratorId={configuratorId}&schedulerCallFor={schedulerCallFor}";

        public string UpdateTaskSchedulerDetailsAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskScheduler/UpdateTaskSchedulerDetails";

        public string DeleteTaskSchedulerAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskScheduler/DeleteTaskScheduler";

        public string GetExecuteTaskSchedulerAsync(DateTime startTime) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskScheduler/GetExecuteTaskScheduler?startTime={startTime}";


    }
}
