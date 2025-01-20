using Coditech.Common.Helper;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public class AccGLSetupNarrationViewModel : BaseViewModel
    {
        public int AccGLSetupNarrationId { get; set; }
        [Required]
        [MaxLength(500)]
        [Display(Name = "Narration Description")]
        public string NarrationDescription { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Narration Type")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string NarrationType { get; set; }
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string CentreCode { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public bool IsSystemGenerated { get; set; }

    }
}
