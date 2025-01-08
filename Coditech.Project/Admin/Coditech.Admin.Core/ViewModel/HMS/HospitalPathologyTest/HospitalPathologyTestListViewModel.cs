using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPathologyTestListViewModel : BaseViewModel
    {
        public List<HospitalPathologyTestViewModel> HospitalPathologyTestList { get; set; }
        public HospitalPathologyTestListViewModel()
        {
            HospitalPathologyTestList = new List<HospitalPathologyTestViewModel>();
        }
    }
}
