namespace Coditech.Common.API.Model
{
    public class GymWorkoutPlanSetModel : BaseModel
    {
        public GymWorkoutPlanSetModel()
        {
            GymWorkoutPlanDetailsList = new List<GymWorkoutPlanDetailsModel>();
        }
        public List<GymWorkoutPlanDetailsModel> GymWorkoutPlanDetailsList { get; set; }
        public long GymWorkoutSetId { get; set; }       
        public long GymWorkoutPlanDetailId { get; set; }
        public decimal? Weight { get; set; } 
        public short? Repetitions { get; set; }
        public short? Duration  { get; set; }
        public string GymWorkoutPlanData { get; set; }
    }
}
