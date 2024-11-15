using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel 
{ 
    public class OrganisationCentrewiseWhatsAppSettingViewModel : BaseViewModel
    {
        [Required]
        [Display(Name = "WhatsApp Setting ID")]
        public int OrganisationCentrewiseWhatsAppSettingId { get; set; }

        [Required]
        public int OrganisationCentreMasterId { get; set; }

        [Required]
        [Display(Name = "WhatsApp Provider ID")]
        public byte GeneralWhatsAppProviderId { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }

        [MaxLength(50)]
        [Display(Name = "WhatsApp Portal Account ID")]
        public string WhatsAppPortalAccountId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Auth Token")]
        public string AuthToken { get; set; }

        [MaxLength(15)]
        [Display(Name = "From Mobile Number")]
        public string FromMobileNumber { get; set; }

        [Required]
        [Display(Name = "Is WhatsApp Setting Enabled")]
        public bool IsWhatsAppSettingEnabled { get; set; }


        [Display(Name = "Centre Name")]
        public string CentreName { get; set; }
    }
}
