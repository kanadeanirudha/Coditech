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
        public short GeneralPolicyMasterId { get; set; }
        public string PolicyCode { get; set; }
        public string SelectedParameter1 { get; set; }
    }
}
