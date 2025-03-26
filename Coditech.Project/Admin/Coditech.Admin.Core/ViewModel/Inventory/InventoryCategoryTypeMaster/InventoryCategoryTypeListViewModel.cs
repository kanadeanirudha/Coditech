using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class InventoryCategoryTypeListViewModel : BaseViewModel
    {
        public List<InventoryCategoryTypeViewModel> InventoryCategoryTypeList { get; set; }
        public InventoryCategoryTypeListViewModel()
        {
            InventoryCategoryTypeList = new List<InventoryCategoryTypeViewModel>();
        }
    }
}
