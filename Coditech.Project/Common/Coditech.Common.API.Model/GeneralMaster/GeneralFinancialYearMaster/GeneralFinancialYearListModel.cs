namespace Coditech.Common.API.Model
{
    public class GeneralFinancialYearListModel : BaseListModel
    {
        public List<GeneralFinancialYearModel> GeneralFinancialYearList { get; set; }
        public GeneralFinancialYearListModel()
        {
            GeneralFinancialYearList = new List<GeneralFinancialYearModel>();
        }

    }
}
