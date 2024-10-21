namespace Coditech.Common.API.Model
{
    public class GymWorkoutPlanSetModel : BaseModel
    {
        public long GymWorkoutSetId { get; set; }
        public string CentreCode { get; set; }
        public long GymWorkoutPlanDetailId { get; set; }
        public decimal Weight { get; set; } 
        public short Repetitions { get; set; }
        public short Duration  { get; set; }        
    }
}
