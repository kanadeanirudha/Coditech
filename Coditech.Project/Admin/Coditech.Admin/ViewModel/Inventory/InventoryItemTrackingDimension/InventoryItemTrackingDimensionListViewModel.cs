using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class InventoryItemTrackingDimensionListViewModel : BaseViewModel
    {
        public List<InventoryItemTrackingDimensionViewModel> InventoryItemTrackingDimensionList { get; set; }
        public InventoryItemTrackingDimensionListViewModel()
        {
            InventoryItemTrackingDimensionList = new List<InventoryItemTrackingDimensionViewModel>();
        }
    }
}
