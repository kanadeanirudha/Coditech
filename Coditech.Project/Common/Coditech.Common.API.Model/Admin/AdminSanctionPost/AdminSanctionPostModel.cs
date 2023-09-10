namespace Coditech.Common.API.Model
{
    public class AdminSanctionPostModel : BaseModel
    {
        public int AdminSanctionPostId { get; set; }
        public Int16 DesignationId { get; set; }
        public Int16 DepartmentId { get; set; }
        public string CentreCode { get; set; }
        public string SanctionPostCode { get; set; }
        public string SanctionedPostDescription { get; set; }
        public Int16 NoOfPost { get; set; }
        public string PostType { get; set; }
        public string DesignationType { get; set; }
        public bool IsActive { get; set; }
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string CentreName { get; set; }
    }
}
