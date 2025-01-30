using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class AccSetupGLBankListViewModel : BaseViewModel
    {
        public List<AccSetupGLBankViewModel> AccSetupGLBankList { get; set; }
        public AccSetupGLBankListViewModel()
        {
            AccSetupGLBankList = new List<AccSetupGLBankViewModel>();
        }
    }
}