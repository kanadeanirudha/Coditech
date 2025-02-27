using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.API.Data;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class TaskSchedulerEndpoint : BaseEndpoint
    {        
        public string CreateBatchTaskSchedulerAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskScheduler/CreateBatchTaskScheduler";

        public string GetBatchTaskSchedulerDetailsAsync(int configuratorId, string schedulerCallFor) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskScheduler/GetBatchTaskSchedulerDetails?configuratorId={configuratorId}&schedulerCallFor={schedulerCallFor}";

        public string UpdateBatchTaskSchedulerDetailsAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/TaskScheduler/UpdateBatchTaskSchedulerDetails";

    }
}
