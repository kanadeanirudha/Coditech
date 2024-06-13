namespace Coditech.Common.API.Model.Response
{
    public class EmployeeServiceListResponse : BaseListResponse
    {
        public List<EmployeeServiceModel> EmployeeServiceList { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
