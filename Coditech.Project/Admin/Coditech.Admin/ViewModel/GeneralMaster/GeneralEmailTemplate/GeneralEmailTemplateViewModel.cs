using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralEmailTemplateViewModel : BaseViewModel
    {
        [Required]
        public short GeneralEmailTemplateId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Email Code")]
        public string EmailTemplateCode { get; set; }

        [Required]
        [Display(Name = "Email Name")]
        public string EmailTemplateName { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Email Template")]
        public string EmailTemplate { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
