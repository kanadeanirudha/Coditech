namespace Coditech.Common.API.Model
{
    public class TaskMasterListModel : BaseListModel
    {
        public List<TaskMasterModel> TaskMasterList { get; set; }
        public TaskMasterListModel()
        {
            TaskMasterList = new List<TaskMasterModel>();
        }
    }
}
