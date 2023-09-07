namespace Coditech.API.Data
{
    public partial class AdminRoleMasterDetails
    {
        public Int16 AdminRoleMasterId { get; set; }
        public int AdminRoleCentreRightId { get; set; }
        public Int16 AdminSactionPostId { get; set; }
        public string AdminRoleCode { get; set; }
        public string SanctPostName { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

