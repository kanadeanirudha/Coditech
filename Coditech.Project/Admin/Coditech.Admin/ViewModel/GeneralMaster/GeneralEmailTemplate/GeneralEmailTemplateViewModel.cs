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
        [Display(Name = "Template Code")]
        public string EmailTemplateCode { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Template Name")]
        public string EmailTemplateName { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Template")]
        public string EmailTemplate { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Module Code")]
        public string ModuleCode { get; set; }

        public bool IsSmsTemplate { get; set; }

        public bool IsWhatsAppTemplate { get; set; }
    }
}
