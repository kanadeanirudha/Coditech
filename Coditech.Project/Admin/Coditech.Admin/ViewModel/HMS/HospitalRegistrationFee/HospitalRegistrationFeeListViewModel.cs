using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class HospitalRegistrationFeeListViewModel : BaseViewModel
    {
        public List<HospitalRegistrationFeeViewModel> HospitalRegistrationFeeList { get; set; }
        public HospitalRegistrationFeeListViewModel()
        {
            HospitalRegistrationFeeList = new List<HospitalRegistrationFeeViewModel>();
        }
        public string SelectedCentreCode { get; set; } = string.Empty;
    }
}
