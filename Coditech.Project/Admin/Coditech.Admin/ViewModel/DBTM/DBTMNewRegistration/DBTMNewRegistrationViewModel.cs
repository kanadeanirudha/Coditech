using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class DBTMNewRegistrationViewModel : BaseViewModel
    {
        public long DBTMNewRegistrationId { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "Centre Name")]
        public string CentreName { get; set; }

        [MaxLength(70)]
        [Required]
        [Display(Name = "Email Address")]
        public string EmailId { get; set; }

        [MaxLength(15)]
        [Required]
        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }

        [Required]
        [Display(Name = "City")]
        public int? GeneralCityMasterId { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "Address")]
        public string CentreAddress { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Pin code")]
        public string Pincode { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Mobile Number")]
        public string CellPhone { get; set; }

        [Required]
        [Display(Name = "Organisation Name")]
        public byte OrganisationId { get; set; }

    }
}
