namespace Coditech.Common.API.Model
{
    public class AdminRoleApplicableDetailsModel : BaseModel
    {
        public int AdminRoleApplicableDetailId { get; set; }
        public int AdminRoleMasterId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? WorkFromDate { get; set; }
        public DateTime? WorkToDate { get; set; }
        public string RoleType { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
        public string PersonCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string AdminRoleCode { get; set; }
        public string SanctionPostName { get; set; }
        public List<EmployeeMasterModel> EmployeeList { get; set; }

    }
}
