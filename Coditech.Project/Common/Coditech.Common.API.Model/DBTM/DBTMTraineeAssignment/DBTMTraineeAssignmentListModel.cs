namespace Coditech.Common.API.Model
{
    public class DBTMTraineeAssignmentListModel : BaseListModel
    {
        public List<DBTMTraineeAssignmentModel> DBTMTraineeAssignmentList { get; set; }
        public DBTMTraineeAssignmentListModel()
        {
            DBTMTraineeAssignmentList = new List<DBTMTraineeAssignmentModel>();
        }
    }
}
