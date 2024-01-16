using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class EmployeeMasterViewModel : BaseViewModel
    {
        public EmployeeMasterViewModel()
        {
        }
        public long EmployeeId { get; set; }
        public long PersonId { get; set; }
        public string PersonCode { get; set; }
        public string UserType { get; set; }
        public int EmployeeDesignationMasterId { get; set; }
        public int OrganisationCentrewiseDepartmentId { get; set; }
        public bool IsEmployeeSmoker { get; set; }        
        public long ReportingEmployeeId { get; set; }
        public string PANCardNumber { get; set; }
        public string UANNumber { get; set; }
        public string PassportNumber { get; set; }        
        public string AdharCardNumber { get; set; }
        // public long CreatedBy { get; set; }
        // public DateTime CreatedDate { get; set; }
        // public long ModifiedBy { get; set; }
        // public DateTime ModifiedDate { get; set; }

    }
}
