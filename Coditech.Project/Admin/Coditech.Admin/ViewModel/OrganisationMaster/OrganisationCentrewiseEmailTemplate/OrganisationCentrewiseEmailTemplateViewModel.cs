using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseEmailTemplateViewModel : BaseViewModel
    {
        [Required]
        public short OrganisationCentrewiseEmailTemplateId { get; set; }

        [Required]
        public short OrganisationCentreMasterId { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Email Template Code")]
        public string EmailTemplateCode { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Email Template")]
        public string EmailTemplate { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public string CentreName { get; set; }
        public bool IsSmsTemplate { get; set; }
        public bool IsWhatsAppTemplate { get; set; }
    }
}
