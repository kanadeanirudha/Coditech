namespace Coditech.Common.API.Model
{
    public class GeneralBatchListModel : BaseListModel
    {
        public List<GeneralBatchModel> GeneralBatchList { get; set; }
        public GeneralBatchListModel()
        {
            GeneralBatchList = new List<GeneralBatchModel>();
        }
    }
}
