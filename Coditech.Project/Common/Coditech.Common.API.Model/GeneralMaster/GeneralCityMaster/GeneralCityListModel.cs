namespace Coditech.Common.API.Model
{
    public class GeneralCityListModel : BaseListModel
    {
        public List<GeneralCityMasterModel> GeneralCityList { get; set; }

        public GeneralCityListModel()
        {
            GeneralCityList = new List<GeneralCityMasterModel>();
        }
    }
}
