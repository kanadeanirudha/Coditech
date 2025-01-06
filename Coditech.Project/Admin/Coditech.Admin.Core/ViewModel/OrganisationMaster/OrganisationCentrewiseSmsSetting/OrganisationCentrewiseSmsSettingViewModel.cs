using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseSmsSettingViewModel : BaseViewModel
    {
        [Required]
        [Display(Name = "SMS Setting ID")]
        public int OrganisationCentrewiseSmsSettingId { get; set; }

        [Required]
        public int OrganisationCentreMasterId { get; set; }

        [Required]
        [Display(Name = "SMS Provider ID")]
        public byte GeneralSmsProviderId { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }

        [MaxLength(50)]
        [Display(Name = "SMS Portal Account ID")]
        public string SmsPortalAccountId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Auth Token")]
        public string AuthToken { get; set; }

        [MaxLength(15)]
        [Display(Name = "From Mobile Number")]
        public string FromMobileNumber { get; set; }

        [Required]
        [Display(Name = "Is SMS Setting Enabled")]
        public bool IsSMSSettingEnabled { get; set; }

        [Display(Name = "Centre Name")]
        public string CentreName { get; set; }
    }
}
