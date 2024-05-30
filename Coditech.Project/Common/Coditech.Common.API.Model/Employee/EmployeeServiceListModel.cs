namespace Coditech.Common.API.Model
{
    public class EmployeeServiceListModel : BaseListModel
    {
        public List<EmployeeServiceModel> EmployeeServiceList { get; set; }
        public EmployeeServiceListModel()
        {
            EmployeeServiceList = new List<EmployeeServiceModel>();
        }
        public long PersonId { get; set; }
        public long EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
