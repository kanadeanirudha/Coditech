using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPathologyTestPricesListViewModel : BaseViewModel
    {
        public List<HospitalPathologyTestPricesViewModel> HospitalPathologyTestPricesList { get; set; }
        public HospitalPathologyTestPricesListViewModel()
        {
            HospitalPathologyTestPricesList = new List<HospitalPathologyTestPricesViewModel>();
        }
    }
}
