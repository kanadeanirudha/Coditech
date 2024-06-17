using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPatientTypeListViewModel : BaseViewModel
    {
        public List<HospitalPatientTypeViewModel> HospitalPatientTypeList { get; set; }
        public HospitalPatientTypeListViewModel()
        {
            HospitalPatientTypeList = new List<HospitalPatientTypeViewModel>();
        }
        
    }
}
