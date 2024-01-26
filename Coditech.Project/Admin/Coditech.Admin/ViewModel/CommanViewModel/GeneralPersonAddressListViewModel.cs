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
        public long PersonId { get; set; }
        public int GymMemberDetailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
