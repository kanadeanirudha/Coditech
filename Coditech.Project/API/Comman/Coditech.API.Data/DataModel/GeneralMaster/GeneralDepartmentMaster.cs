namespace Coditech.API.Data
{
    public partial class GeneralDepartmentMaster : BaseDataModel
    {
        public short GeneralDepartmentMasterId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentShortCode { get; set; }
        public string PrintShortDesc { get; set; }
        public Nullable<bool> WorkActivity { get; set; }
    }
}
