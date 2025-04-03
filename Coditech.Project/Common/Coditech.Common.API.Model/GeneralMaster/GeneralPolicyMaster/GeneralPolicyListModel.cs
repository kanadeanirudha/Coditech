namespace Coditech.Common.API.Model
{
    public class GeneralPolicyListModel : BaseListModel
    {
        public List<GeneralPolicyModel> GeneralPolicyList { get; set; }
        public GeneralPolicyListModel()
        {
            GeneralPolicyList = new List<GeneralPolicyModel>();
        }

    }
}
