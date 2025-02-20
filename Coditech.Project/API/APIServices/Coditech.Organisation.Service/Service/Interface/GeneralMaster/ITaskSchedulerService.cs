using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface ITaskSchedulerService
    {
        TaskSchedulerModel CreateBatchTaskScheduler(TaskSchedulerModel model);
        TaskSchedulerModel GetBatchTaskSchedulerDetails(int configuratorId, string schedulerCallFor);
        bool UpdateBatchTaskSchedulerDetails(TaskSchedulerModel model);

       // bool DeleteBatchTaskScheduler(ParameterModel parameterModel);       
    }
}
