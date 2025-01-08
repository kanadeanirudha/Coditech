using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class InventoryCategoryListViewModel : BaseViewModel
    {
        public List<InventoryCategoryViewModel> InventoryCategoryList { get; set; }
        public InventoryCategoryListViewModel()
        {
            InventoryCategoryList = new List<InventoryCategoryViewModel>();
        }
    }
}
