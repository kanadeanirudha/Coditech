using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public class AccGLSetupNarrationViewModel : BaseViewModel
    {
        public int AccGLSetupNarrationId { get; set; }
        
        [MaxLength(500)]
        [Display(Name = "Narration Description")]
        public string NarrationDescription { get; set; }
        [MaxLength(1)]
        [Display(Name = "Narration Type")]
        public string NarrationType { get; set; }       
        
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

    }
}
