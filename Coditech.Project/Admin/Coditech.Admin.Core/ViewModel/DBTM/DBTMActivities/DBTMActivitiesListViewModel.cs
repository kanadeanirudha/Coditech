using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class DBTMActivitiesListViewModel : BaseViewModel
    {
        public List<DBTMActivitiesViewModel> ActivitiesList { get; set; }
        public DBTMActivitiesListViewModel()
        {
            ActivitiesList = new List<DBTMActivitiesViewModel>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
