using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPersonAddressViewModel : BaseViewModel
    {
        public long GeneralPersonAddressId { get; set; }
        public long EntityId { get; set; }
        public string EntityType { get; set; }
        public string AddressTypeEnum { get; set; }
        public long PersonId { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [MaxLength(200)]
        [Required]
        [Display(Name = "Address1")]
        public string AddressLine1 { get; set; }

        [MaxLength(200)]
        [Display(Name = "Address2")]
        public string AddressLine2 { get; set; }

        [MaxLength(100)]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Country")]
        public short GeneralCountryMasterId { get; set; }

        [Required]
        [Display(Name = "Region")]
        public short GeneralRegionMasterId { get; set; }

        [Required]
        [Display(Name = "City")]
        public int GeneralCityMasterId { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Postal Code")]
        public string Postalcode { get; set; }

        [MaxLength(50)]
        [Display(Name = "Telephone Number")]
        public string PhoneNumber { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter valid mobile number")]
        [MaxLength(15)]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [MaxLength(250)]
        [EmailAddress]
        [Display(Name = "Email Id")]
        public string EmailAddress { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Is Correspondance Address Same As Permanent Address?")]
        public bool IsCorrespondanceAddressSameAsPermanentAddress { get; set; }
        public bool IsDefault { get; set; }
        public string ControllerName { get; set; }
        public string AddressData { get; set; }
        public List<BindAddressToPostalCodeViewModel> BindAddressToPostalCodeList { get; set; }

    }
}
