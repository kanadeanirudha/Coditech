namespace Coditech.API.Data
{
    public partial class AdminSactionPostDetails
    {
        public short AdminSactionPostDetailsId { get; set; }
        public int AdminRoleCentreRightId { get; set; }
        public short AdminRoleMasterId { get; set; }
        public string CentreCode { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

