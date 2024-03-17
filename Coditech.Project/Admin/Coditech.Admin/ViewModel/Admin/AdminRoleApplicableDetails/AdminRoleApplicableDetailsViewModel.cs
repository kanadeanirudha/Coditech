using Coditech.Common.API.Model;
using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class AdminRoleApplicableDetailsViewModel : BaseViewModel
    {
        public int AdminRoleApplicableDetailId { get; set; }
        public int AdminRoleMasterId { get; set; }

        [Required]
        [Display(Name = "Employee List")]
        public int EmployeeId { get; set; }
        [Display(Name = "Work From Date")]
        public DateTime? WorkFromDate { get; set; }
        [Display(Name = "Work To Date")]
        public DateTime? WorkToDate { get; set; }
        public string RoleType { get; set; }
        public string Reason { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public string PersonCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }

        [Display(Name = "Admin Role Code")]
        public string AdminRoleCode { get; set; }
        [Display(Name = "Role Description")]
        public string SanctionPostName { get; set; }
        public List<EmployeeMasterModel> EmployeeList { get; set; }
    }
}
