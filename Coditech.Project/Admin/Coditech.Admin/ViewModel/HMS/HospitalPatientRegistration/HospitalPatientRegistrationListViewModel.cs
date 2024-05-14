using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPatientRegistrationListViewModel : BaseViewModel
    {
        public List<HospitalPatientRegistrationViewModel> HospitalPatientRegistrationList { get; set; }
        public HospitalPatientRegistrationListViewModel()
        {
            HospitalPatientRegistrationList = new List<HospitalPatientRegistrationViewModel>();
        }
        public string SelectedCentreCode { get; set; } = string.Empty;
    }
}
