using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class InventoryProductDimensionListViewModel:BaseViewModel
    {
        public List<InventoryProductDimensionViewModel> InventoryProductDimensionList { get; set; }
        public InventoryProductDimensionListViewModel()
        {
            InventoryProductDimensionList = new List<InventoryProductDimensionViewModel>();
        }
    }
}
