namespace Coditech.Common.API.Model
{
    public class GeneralDistrictListModel : BaseListModel
    {
        public List<GeneralDistrictModel> GeneralDistrictList { get; set; }
        public GeneralDistrictListModel()
        {
            GeneralDistrictList = new List<GeneralDistrictModel>();
        }

    }
}
