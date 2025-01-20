using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class DBTMActivitiesDetailsListViewModel : BaseViewModel
    {
        public List<DBTMActivitiesDetailsViewModel> ActivitiesDetailsList { get; set; }
        public DBTMActivitiesDetailsListViewModel()
        {
            ActivitiesDetailsList = new List<DBTMActivitiesDetailsViewModel>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
