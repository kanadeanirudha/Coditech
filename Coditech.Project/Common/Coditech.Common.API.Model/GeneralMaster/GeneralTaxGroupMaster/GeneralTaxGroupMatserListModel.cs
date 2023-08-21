namespace Coditech.Common.API.Model
{
    public class GeneralTaxGroupMasterListModel : BaseListModel
    {
        public List<GeneralTaxGroupMasterModel> GeneralTaxGroupMasterList { get; set; }
        public GeneralTaxGroupMasterListModel()
        {
            GeneralTaxGroupMasterList = new List<GeneralTaxGroupMasterModel>();
        }
    }
}
