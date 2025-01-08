using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class AccSetupMasterListViewModel : BaseViewModel
    {
        public List<AccSetupMasterViewModel> AccSetupMasterList { get; set; }
        public AccSetupMasterListViewModel()
        {
            AccSetupMasterList = new List<AccSetupMasterViewModel>();
        }
        public string SelectedCentreCode { get; set; }
    }
}