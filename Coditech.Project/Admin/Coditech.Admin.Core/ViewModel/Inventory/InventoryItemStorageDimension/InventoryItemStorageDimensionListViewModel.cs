using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class InventoryItemStorageDimensionListViewModel : BaseViewModel
    {
        public List<InventoryItemStorageDimensionViewModel> InventoryItemStorageDimensionList { get; set; }
        public InventoryItemStorageDimensionListViewModel()
        {
            InventoryItemStorageDimensionList = new List<InventoryItemStorageDimensionViewModel>();
        }
    }
}
