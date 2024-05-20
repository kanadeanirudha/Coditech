using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class InventoryItemGroupListViewModel : BaseViewModel
    {
        public List<InventoryItemGroupViewModel> InventoryItemGroupList { get; set; }
        public InventoryItemGroupListViewModel()
        {
            InventoryItemGroupList = new List<InventoryItemGroupViewModel>();
        }
    }
}
