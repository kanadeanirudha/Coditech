using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface ITaskSchedulerService
    {
        TaskSchedulerListModel GetTaskSchedulerList();
        TaskSchedulerModel CreateTaskScheduler(TaskSchedulerModel model);
        TaskSchedulerModel GetTaskSchedulerDetails(int configuratorId, string schedulerCallFor);
        bool UpdateTaskSchedulerDetails(TaskSchedulerModel model);
        bool DeleteTaskScheduler(ParameterModel parameterModel);
        TaskSchedulerModel ExecuteTaskScheduler(DateTime startTime);
    }
}
