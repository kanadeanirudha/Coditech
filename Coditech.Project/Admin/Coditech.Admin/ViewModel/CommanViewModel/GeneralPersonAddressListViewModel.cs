using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPersonAddressListViewModel : BaseViewModel
    {
        public List<GeneralPersonAddressViewModel> GeneralPersonAddressList { get; set; }
        public GeneralPersonAddressListViewModel()
        {
            GeneralPersonAddressList = new List<GeneralPersonAddressViewModel>();
        }
    }
}
