using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPolicyDetailsListViewModel : BaseViewModel
    {
        public List<GeneralPolicyDetailsViewModel> GeneralPolicyDetailsList { get; set; }
        public GeneralPolicyDetailsListViewModel()
        {
            GeneralPolicyDetailsList = new List<GeneralPolicyDetailsViewModel>();
        }
        public string CentreCode { get; set; } = string.Empty;
    }
}
