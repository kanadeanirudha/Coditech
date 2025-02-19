namespace Coditech.Common.API.Model
{
    public class TaskSchedulerListModel : BaseListModel
    {
        public List<TaskSchedulerModel> TaskSchedulerList { get; set; }
        public TaskSchedulerListModel()
        {
            TaskSchedulerList = new List<TaskSchedulerModel>();
        }
    }
}

