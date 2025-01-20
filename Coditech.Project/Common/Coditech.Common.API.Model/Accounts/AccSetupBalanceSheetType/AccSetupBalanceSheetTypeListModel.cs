namespace Coditech.Common.API.Model
{
    public partial class AccSetupBalanceSheetTypeListModel : BaseListModel
    {
        public List<AccSetupBalanceSheetTypeModel> AccSetupBalanceSheetTypeList { get; set; }
        public AccSetupBalanceSheetTypeListModel()
        {
            AccSetupBalanceSheetTypeList = new List<AccSetupBalanceSheetTypeModel>();
        }
    }
}
