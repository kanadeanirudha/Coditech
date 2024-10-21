using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GymWorkoutPlanDetailsListViewModel : BaseViewModel
    {
        public List<GymWorkoutPlanDetailsViewModel> GymWorkoutPlanDetailsList { get; set; }
        public GymWorkoutPlanDetailsListViewModel()
        {
            GymWorkoutPlanDetailsList = new List<GymWorkoutPlanDetailsViewModel>();
        }
        public long GymWorkoutPlanId { get; set; }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public long PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
