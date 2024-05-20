using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class InventoryUoMMasterListViewModel : BaseViewModel
    {
        public List<InventoryUoMMasterViewModel> InventoryUoMMasterList { get; set; }
        public InventoryUoMMasterListViewModel()
        {
            InventoryUoMMasterList = new List<InventoryUoMMasterViewModel>();
        }
    }
}
