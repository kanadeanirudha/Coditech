using Coditech.Common.API.Model;

namespace Coditech.Hangfire
{
    public interface ISchedulerProviders
    {
        //Invokes the methods under the schedulers
        void InvokeMethod(TaskSchedulerModel model);
    }
}
