using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class InventoryItemModelGroupListViewModel : BaseViewModel
    {
        public List<InventoryItemModelGroupViewModel> InventoryItemModelGroupList { get; set; }
        public InventoryItemModelGroupListViewModel()
        {
            InventoryItemModelGroupList = new List<InventoryItemModelGroupViewModel>();
        }
    }
}
