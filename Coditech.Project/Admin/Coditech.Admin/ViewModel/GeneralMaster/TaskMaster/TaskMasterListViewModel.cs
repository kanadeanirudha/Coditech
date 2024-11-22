using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class TaskMasterListViewModel : BaseViewModel
    {
        public List<TaskMasterViewModel> TaskMasterList { get; set; }
        public TaskMasterListViewModel()
        {
            TaskMasterList = new List<TaskMasterViewModel>();
        }
    }
}
