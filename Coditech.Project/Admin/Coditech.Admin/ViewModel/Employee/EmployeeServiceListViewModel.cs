using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class EmployeeServiceListViewModel : BaseViewModel
    {
        public List<EmployeeServiceViewModel> EmployeeServiceList { get; set; }
        public EmployeeServiceListViewModel()
        {
            EmployeeServiceList = new List<EmployeeServiceViewModel>();
        }
        public long PersonId { get; set; }
        public long EmployeeId { get; set; }
        public long EntityId { get; set; }
        public string EntityType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
