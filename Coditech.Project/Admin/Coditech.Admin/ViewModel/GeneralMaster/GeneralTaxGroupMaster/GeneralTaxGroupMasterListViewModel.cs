using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralTaxGroupMasterListViewModel : BaseViewModel
    {
        public List<GeneralTaxGroupMasterViewModel> GeneralTaxGroupMasterList { get; set; }

        public GeneralTaxGroupMasterListViewModel()
        {
            GeneralTaxGroupMasterList = new List<GeneralTaxGroupMasterViewModel>();
        }
    }
}
