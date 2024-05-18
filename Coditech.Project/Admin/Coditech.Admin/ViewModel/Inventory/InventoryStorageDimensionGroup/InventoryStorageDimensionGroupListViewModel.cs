using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class InventoryStorageDimensionGroupListViewModel : BaseViewModel
    {
        public List<InventoryStorageDimensionGroupViewModel> InventoryStorageDimensionGroupList { get; set; }
        public InventoryStorageDimensionGroupListViewModel()
        {
            InventoryStorageDimensionGroupList = new List<InventoryStorageDimensionGroupViewModel>();
        }
    }
}
