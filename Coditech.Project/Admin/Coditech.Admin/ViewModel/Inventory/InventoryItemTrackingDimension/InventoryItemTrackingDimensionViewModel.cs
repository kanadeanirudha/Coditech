using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class InventoryItemTrackingDimensionViewModel : BaseViewModel
    {
        public short InventoryItemTrackingDimensionId { get; set; }
        [Display(Name = "Tracking Dimension Name")]
        public string TrackingDimensionName { get; set; }
        [Display(Name = "Tracking Dimension Code")]
        public string TrackingDimensionCode { get; set; }
    }
}
