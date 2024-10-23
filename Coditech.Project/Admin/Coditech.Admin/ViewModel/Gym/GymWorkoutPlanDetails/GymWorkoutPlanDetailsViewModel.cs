using Coditech.Common.API.Model;
using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GymWorkoutPlanDetailsViewModel : BaseViewModel
    {
        public GymWorkoutPlanModel GymWorkoutPlanModel { get; set; }
        public string SelectedCentreCode { get; set; } = string.Empty;
    }
}
