namespace Coditech.API.Data
{
    public partial class AdminSanctionPost
    {
        public int AdminSanctionPostId { get; set; }
        public short DesignationId { get; set; }
        public short NoOfPost { get; set; }
        public short DepartmentId { get; set; }
        public bool IsActive { get; set; }
        public string CentreCode { get; set; }
        public string DesignationType { get; set; }
        public string SanctionPostCode { get; set; }
        public string PostType { get; set; }
        public string SanctionedPostDescription { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

