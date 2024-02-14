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
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CompanyName { get; set; }
        public int GeneralRegionMasterId { get; set; }
        public int GeneralCityMasterId { get; set; }
        public string Postalcode { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public bool IsCorrespondanceAddressSameAsPermanentAddress { get; set; }
    }
}
