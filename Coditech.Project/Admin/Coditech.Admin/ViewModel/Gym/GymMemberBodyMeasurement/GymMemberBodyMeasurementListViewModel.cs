using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GymMemberBodyMeasurementListViewModel : BaseViewModel
    {
        public List<GymMemberBodyMeasurementViewModel> GymMemberBodyMeasurementList { get; set; }
        public GymMemberBodyMeasurementListViewModel()
        {
            GymMemberBodyMeasurementList = new List<GymMemberBodyMeasurementViewModel>();
        }
    }
}
