using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPolicyListViewModel : BaseViewModel
    {
        public List<GeneralPolicyViewModel> GeneralPolicyList { get; set; }
        public GeneralPolicyListViewModel()
        {
            GeneralPolicyList = new List<GeneralPolicyViewModel>();
        }
    }
}
