using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralBatchViewModel : BaseViewModel
    {
        public int GeneralBatchMasterId { get; set; }
        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string CentreCode { get; set; }
        [Required]
        [Display(Name = "Batch Name")]
        public string BatchName { get; set; }
        [Required]
        [Display(Name = "Batch Time")]
        public TimeSpan? BatchTime { get; set; }
        [Required]
        [Display(Name = "Batch Start Time")]
        public TimeSpan? BatchStartTime { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
