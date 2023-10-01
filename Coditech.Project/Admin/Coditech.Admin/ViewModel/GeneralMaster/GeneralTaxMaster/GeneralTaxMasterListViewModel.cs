using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralTaxMasterListViewModel : BaseViewModel
    {
        public List<GeneralTaxMasterViewModel> GeneralTaxMasterList { get; set; }
        public GeneralTaxMasterListViewModel()
        {
            GeneralTaxMasterList = new List<GeneralTaxMasterViewModel>();
        }
    }
}
