namespace Coditech.Common.API.Model
{
    public class GeneralPolicyRulesListModel : BaseListModel
    {
        public List<GeneralPolicyRulesModel> GeneralPolicyRulesList { get; set; }
        public GeneralPolicyRulesListModel()
        {
            GeneralPolicyRulesList = new List<GeneralPolicyRulesModel>();
        }
    }
}
