using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Coditech.Resources;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GymWorkoutPlanDetailsViewModel : BaseViewModel
    {
        public long GymWorkoutPlanDetailId { get; set; }
        public long GymWorkoutPlanId { get; set; }

        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string CentreCode { get; set; }

        [Display(Name = "Workout Name")]
        public string WorkoutName { get; set; }

        [Display(Name = "Week")]
        public short WorkoutWeek { get; set; }

        [Display(Name = "Day")]
        public byte WorkoutDay { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PersonId { get; set; }

    }


}
