using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class InventoryProductDimensionGroupListViewModel : BaseViewModel
    {
        public List<InventoryProductDimensionGroupViewModel> InventoryProductDimensionGroupList { get; set; }
        public InventoryProductDimensionGroupListViewModel()
        {
            InventoryProductDimensionGroupList = new List<InventoryProductDimensionGroupViewModel>();
        }
    }
}
