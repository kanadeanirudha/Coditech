using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPersonAddressViewModel : BaseViewModel
    {
        public long GeneralPersonAddressId { get; set; }
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
        [Display(Name ="Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Country")]
        [Required]
        public short GeneralCountryMasterId { get; set; }

        [Display(Name = "Region")]
        [Required]
        public short GeneralRegionMasterId { get; set; }
        [Display(Name = "City")]
        [Required]
        public int GeneralCityMasterId { get; set; }
        [MaxLength(10)]
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
        public bool IsActive { get; set; }
        [Display(Name ="Same as Permanent Address")]
        public bool IsCorrespondanceAddressSameAsPermanentAddress { get; set; }
        public bool IsDefault { get; set; }
    }
}
