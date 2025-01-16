namespace Coditech.Common.API.Model
{
    public class DBTMActivitiesListModel : BaseListModel
    {
        public List<DBTMActivitiesModel> ActivitiesList { get; set; }
        public DBTMActivitiesListModel()
        {
            ActivitiesList = new List<DBTMActivitiesModel>();
        }
    }
}
