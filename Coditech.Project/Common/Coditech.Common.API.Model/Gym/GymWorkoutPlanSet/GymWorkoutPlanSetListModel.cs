namespace Coditech.Common.API.Model
{
    public class GymWorkoutPlanSetListModel : BaseListModel
    {
        public List<GymWorkoutPlanSetModel> GymWorkoutPlanSetList { get; set; }
        public GymWorkoutPlanSetListModel()
        {
            GymWorkoutPlanSetList = new List<GymWorkoutPlanSetModel>();
        }

    }
}


