using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class InventoryGeneralItemMasterListViewModel : BaseViewModel
    {
        public List<InventoryGeneralItemMasterViewModel> InventoryGeneralItemMasterList { get; set; }
        public InventoryGeneralItemMasterListViewModel()
        {
            InventoryGeneralItemMasterList = new List<InventoryGeneralItemMasterViewModel>();
        }
    }
}
