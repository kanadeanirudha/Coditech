
namespace Coditech.API.Data
{
    public class GeneralDepartmentMaster
    {
        public short GeneralDepartmentMasterId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentShortCode { get; set; }
        public string PrintShortDesc { get; set; }
        public Nullable<bool> WorkActivity { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
