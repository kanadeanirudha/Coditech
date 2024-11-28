using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class TaskApprovalSettingEmployeeViewModel : BaseViewModel
    {        
        public long EmployeeId { get; set; }
        public byte CountNumber { get; set; }           
        public List<EmployeeMasterModel> EmployeeList { get; set; }
    }
}
