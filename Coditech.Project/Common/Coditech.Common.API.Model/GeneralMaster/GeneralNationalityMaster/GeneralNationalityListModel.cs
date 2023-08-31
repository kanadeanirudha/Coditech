namespace Coditech.Common.API.Model
{
    public class GeneralNationalityListModel : BaseListModel
    {
        public List<GeneralNationalityModel> GeneralNationalityList { get; set; }
        public GeneralNationalityListModel()
        {
            GeneralNationalityList = new List<GeneralNationalityModel>();
        }
    }
}
