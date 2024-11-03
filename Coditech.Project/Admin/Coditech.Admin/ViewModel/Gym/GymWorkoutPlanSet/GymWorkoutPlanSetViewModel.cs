using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Coditech.Resources;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GymWorkoutPlanSetViewModel : BaseViewModel
    {
        public GymWorkoutPlanSetViewModel()
        {
            GymWorkoutPlanSetList = new List<GymWorkoutPlanSetModel>();
        }
        public List<GymWorkoutPlanSetModel> GymWorkoutPlanSetList { get; set; }
        public long GymWorkoutSetId { get; set; }
        public long GymWorkoutPlanDetailId { get; set; }
            
        [Required]
        [Display(Name = "Weight")]
        public decimal? Weight { get; set; }

        [Required]
        [Display(Name = "Repetitions")]
        public short? Repetitions { get; set; }

        [Required]
        [Display(Name = "Duration")]
        public short? Duration { get; set; }
        [Display(Name = "Workout Plan Name")]
        public string WorkoutPlanName { get; set; }
        public string GymWorkoutPlanData { get; set; }

        public GymWorkoutPlanDetailsModel GymWorkoutPlanDetailsModel { get; set; }
        
        
        

    }
}
