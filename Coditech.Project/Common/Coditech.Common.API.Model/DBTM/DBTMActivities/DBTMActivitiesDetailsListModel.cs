namespace Coditech.Common.API.Model
{
    public class DBTMActivitiesDetailsListModel : BaseListModel
    {
        public List<DBTMActivitiesDetailsModel> ActivitiesDetailsList { get; set; }
        public DBTMActivitiesDetailsListModel()
        {
            ActivitiesDetailsList = new List<DBTMActivitiesDetailsModel>();
        }
        public string PersonCode { get; set; }
        public long PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
