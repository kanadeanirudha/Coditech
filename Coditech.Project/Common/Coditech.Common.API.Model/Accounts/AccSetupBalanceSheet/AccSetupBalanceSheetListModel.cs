namespace Coditech.Common.API.Model
{
    public partial class AccSetupBalanceSheetListModel : BaseListModel
    {
        public List<AccSetupBalanceSheetModel> AccSetupBalanceSheetList { get; set; }
        public AccSetupBalanceSheetListModel()
        {
            AccSetupBalanceSheetList = new List<AccSetupBalanceSheetModel>();
        }
    }
}

