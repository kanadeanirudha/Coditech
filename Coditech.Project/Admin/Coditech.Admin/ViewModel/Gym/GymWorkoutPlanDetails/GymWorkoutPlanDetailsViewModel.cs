using Coditech.Common.API.Model;
using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GymWorkoutPlanDetailsViewModel : BaseViewModel
    {
        public GymWorkoutPlanModel GymWorkoutPlanModel { get; set; }
        public long GymWorkoutPlanDetailId { get; set; }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public List<GymWorkoutPlanSetViewModel> Sets { get; set; } = new List<GymWorkoutPlanSetViewModel>();
    }
}
