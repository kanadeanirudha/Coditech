
namespace Coditech.Common.API.Model
{
    public class EmployeeMasterModel : BaseModel
    {
        public long EmployeeId { get; set; }
        public long PersonId { get; set; }
        public string PersonCode { get; set; }
        public string UserType { get; set; }
        public int EmployeeDesignationMasterId { get; set; }
        public int OrganisationCentrewiseDepartmentId { get; set; }
        public bool IsEmployeeSmoker { get; set; }
        public int ReportingEmployeeId { get; set; }
        public string PANCardNumber { get; set; }
        public string UANNumber { get; set; }
        public string PassportNumber { get; set; }
        public string AdharCardNumber { get; set; }
    }
}
