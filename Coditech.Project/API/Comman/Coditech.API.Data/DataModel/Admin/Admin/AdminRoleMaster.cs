using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class AdminRoleMaster
    {
        [Key]
        public int AdminRoleMasterId { get; set; }
        public int AdminSanctionPostId { get; set; }
        public string SanctionPostName { get; set; }
        public string MonitoringLevel { get; set; }
        public string AdminRoleCode { get; set; }
        public string OthCentreLevel { get; set; }
        public bool IsLoginAllowFromOutside { get; set; }
        public bool IsAttendaceAllowFromOutside { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

