namespace Coditech.Common.API.Model
{
    public class GeneralTaxMasterListModel : BaseListModel
    {
        public List<GeneralTaxMasterModel> GeneralTaxMasterList { get; set; }
        public GeneralTaxMasterListModel()
        {
            GeneralTaxMasterList = new List<GeneralTaxMasterModel>();
        }
    }
}
