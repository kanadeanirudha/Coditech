using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

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
        public long EntityId { get; set; }
        public string EntityType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ControllerName { get; set; }

        [Display(Name = "Same as Permanent Address")]
        public bool IsCorrespondanceAddressSameAsPermanentAddress { get; set; }
    }
}
