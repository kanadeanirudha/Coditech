using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class AdminRoleApplicableDetail
    {
        [Key]
        public int AdminRoleApplicableDetailId { get; set; }
        public int AdminRoleMasterId { get; set; }
        public int EmployeeId { get; set; }
        public int DesignationId { get; set; }
        public Nullable<System.DateTime> WorkFromDate { get; set; }
        public Nullable<System.DateTime> WorkToDate { get; set; }
        public string RoleType { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
