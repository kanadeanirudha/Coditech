using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class InventoryItemTrackingDimensionGroupListViewModel : BaseViewModel
    {
        public List<InventoryItemTrackingDimensionGroupViewModel> InventoryItemTrackingDimensionGroupList { get; set; }
        public InventoryItemTrackingDimensionGroupListViewModel()
        {
            InventoryItemTrackingDimensionGroupList = new List<InventoryItemTrackingDimensionGroupViewModel>();
        }
    }
}
