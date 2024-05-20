namespace Coditech.Common.API.Model
{
    public class EmployeeServiceListModel : BaseListModel
    {
        public List<EmployeeServiceModel> EmployeeServiceList { get; set; }
        public EmployeeServiceListModel()
        {
            EmployeeServiceList = new List<EmployeeServiceModel>();
        }

    }
}
