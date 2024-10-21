using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class InventoryItemTrackingDimensionViewModel : BaseViewModel
    {
        public short InventoryItemTrackingDimensionId { get; set; }
        [Display(Name = "Tracking Dimension Name")]
        [Required]
        public string TrackingDimensionName { get; set; }
        [Display(Name = "Tracking Dimension Code")]
        [Required]
        public string TrackingDimensionCode { get; set; }
    }
}
