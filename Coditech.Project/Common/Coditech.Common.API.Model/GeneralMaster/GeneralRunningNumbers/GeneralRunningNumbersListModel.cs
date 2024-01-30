namespace Coditech.Common.API.Model
{
    public class GeneralRunningNumbersListModel : BaseListModel
    {
        public List<GeneralRunningNumbersModel> GeneralRunningNumbersList { get; set; }
        public GeneralRunningNumbersListModel()
        {
            GeneralRunningNumbersList = new List<GeneralRunningNumbersModel>();
        }

    }
}
