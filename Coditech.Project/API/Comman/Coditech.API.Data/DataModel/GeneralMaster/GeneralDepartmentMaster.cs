namespace Coditech.API.Data
{
    public partial class GeneralDepartmentMaster
    {
        public short GeneralDepartmentMasterId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentShortCode { get; set; }
        public string PrintShortDesc { get; set; }
        public Nullable<bool> WorkActivity { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
