using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class InventoryItemTrackingDimensionGroupViewModel : BaseViewModel
    {
        public InventoryItemTrackingDimensionGroupViewModel()
        {
            InventoryItemTrackingDimensionGroupMapperList = new List<InventoryItemTrackingDimensionGroupMapperModel>();
        }
        public List<InventoryItemTrackingDimensionGroupMapperModel> InventoryItemTrackingDimensionGroupMapperList { get; set; }
        public int InventoryItemTrackingDimensionGroupId { get; set; }

        [Required]
        [Display(Name = "ItemTracking Dimension Group Name")]
        public string ItemTrackingDimensionGroupName { get; set; }

        [Required]
        [Display(Name = "ItemTracking Dimension Group Code")]
        public string ItemTrackingDimensionGroupCode { get; set; }
        public string ItemTrackingDimensionGroupMapperData { get; set; }
    }
}
