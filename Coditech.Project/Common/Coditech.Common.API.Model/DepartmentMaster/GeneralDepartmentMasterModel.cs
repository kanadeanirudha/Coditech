namespace Coditech.Common.API.Model
{
    public class GeneralDepartmentMasterModel : BaseModel
    {
        public GeneralDepartmentMasterModel()
        {

        }
        public short GeneralDepartmentMasterId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentShortCode { get; set; }
        public string PrintShortDesc { get; set; }
        public bool WorkActivity { get; set; }
    }
}
