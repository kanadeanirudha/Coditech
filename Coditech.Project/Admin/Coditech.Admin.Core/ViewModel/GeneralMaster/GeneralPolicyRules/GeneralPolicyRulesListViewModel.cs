using Coditech.Common.API.Model;
using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPolicyRulesListViewModel : BaseViewModel
    {
        public List<GeneralPolicyRulesViewModel> GeneralPolicyRulesList { get; set; }
        public GeneralPolicyRulesListViewModel()
        {
            GeneralPolicyRulesList = new List<GeneralPolicyRulesViewModel>();
        }
        public string PolicyCode { get; set; }
        public string PolicyApplicableStatus { get; set; }
        public GeneralPolicyDetailsModel GeneralPolicyDetails { get; set; }
    }
}
