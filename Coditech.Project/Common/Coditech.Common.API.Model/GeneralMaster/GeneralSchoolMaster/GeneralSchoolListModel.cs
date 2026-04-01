namespace Coditech.Common.API.Model
{
    public class GeneralSchoolListModel : BaseListModel
    {
        public List<GeneralSchoolModel> GeneralSchoolList { get; set; }
        public GeneralSchoolListModel()
        {
            GeneralSchoolList = new List<GeneralSchoolModel>();
        }
    }
}
