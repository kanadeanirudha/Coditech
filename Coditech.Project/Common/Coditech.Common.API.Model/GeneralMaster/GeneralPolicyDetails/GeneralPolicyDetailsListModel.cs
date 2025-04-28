namespace Coditech.Common.API.Model
{
    public class GeneralPolicyDetailsListModel : BaseListModel
    {
        public List<GeneralPolicyDetailsModel> GeneralPolicyDetailsList { get; set; }
        public GeneralPolicyDetailsListModel()
        {
            GeneralPolicyDetailsList = new List<GeneralPolicyDetailsModel>();
        }
    }
}
