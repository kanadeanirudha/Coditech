namespace Coditech.Common.API.Model
{
    public class GymWorkoutPlanDetailsModel : BaseModel
    {
        public long GymWorkoutPlanDetailId { get; set; }
        public string CentreCode { get; set; }
        public long GymWorkoutPlanId { get; set; } 
        public string WorkoutName { get; set; }
        public short WorkoutWeek { get; set; }
        public byte WorkoutDay { get; set; }
    }
}
