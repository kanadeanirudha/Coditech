using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPathologyTestGroupListViewModel : BaseViewModel
    {
        public List<HospitalPathologyTestGroupViewModel> HospitalPathologyTestGroupList { get; set; }
        public HospitalPathologyTestGroupListViewModel()
        {
            HospitalPathologyTestGroupList = new List<HospitalPathologyTestGroupViewModel>();
        }
    }
}
