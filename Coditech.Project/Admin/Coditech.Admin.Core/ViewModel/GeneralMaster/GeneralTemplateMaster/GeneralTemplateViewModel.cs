using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralTemplateViewModel : BaseViewModel
    {
        public int GeneralTemplateMasterId { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Template Code")]
        public string TemplateCode { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Template Name")]
        public string TemplateName { get; set; }
        [Required]
        [Display(Name = "Template Type")]
        public string TemplateType { get; set; }
    }
}
