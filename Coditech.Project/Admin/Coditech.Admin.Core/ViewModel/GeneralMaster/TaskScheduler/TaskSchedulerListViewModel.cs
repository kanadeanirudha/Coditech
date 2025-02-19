using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class TaskSchedulerListViewModel : BaseViewModel
    {
        public List<TaskSchedulerViewModel> TaskSchedulerList { get; set; }
        public TaskSchedulerListViewModel()
        {
            TaskSchedulerList = new List<TaskSchedulerViewModel>();
        }
    }
}
