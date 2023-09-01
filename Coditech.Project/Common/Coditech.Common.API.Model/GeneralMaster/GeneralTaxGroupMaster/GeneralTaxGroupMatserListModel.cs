namespace Coditech.Common.API.Model
{
    public class GeneralTaxGroupMasterListModel : BaseListModel
    {
        public List<GeneralTaxGroupModel> GeneralTaxGroupMasterList { get; set; }
        public GeneralTaxGroupMasterListModel()
        {
            GeneralTaxGroupMasterList = new List<GeneralTaxGroupModel>();
        }
    }
}
