namespace Coditech.Common.API.Model
{
    public class GeneralRegionListModel : BaseListModel
    {
        public List<GeneralRegionModel> GeneralRegionList { get; set; }
        public GeneralRegionListModel()
        {
            GeneralRegionList = new List<GeneralRegionModel>();
        }

    }
}
