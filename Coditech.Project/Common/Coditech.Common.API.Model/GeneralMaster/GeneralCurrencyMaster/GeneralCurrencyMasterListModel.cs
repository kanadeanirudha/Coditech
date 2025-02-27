namespace Coditech.Common.API.Model
{
    public class GeneralCurrencyMasterListModel : BaseListModel
    {
        public List<GeneralCurrencyMasterModel> GeneralCurrencyMasterList { get; set; }
        public GeneralCurrencyMasterListModel()
        {
            GeneralCurrencyMasterList = new List<GeneralCurrencyMasterModel>();
        }

    }
}
