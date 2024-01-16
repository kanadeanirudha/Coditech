namespace Coditech.Common.API.Model
{
    public class EmployeeMasterListModel : BaseListModel
    {
        public List<EmployeeMasterModel> EmployeeMasterList { get; set; }
        public EmployeeMasterListModel()
        {
            EmployeeMasterList = new List<EmployeeMasterModel>();
        }

    }
}
