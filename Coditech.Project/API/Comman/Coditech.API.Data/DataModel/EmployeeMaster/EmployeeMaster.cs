using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class EmployeeMaster
    {
        [Key]
        public long EmployeeId { get; set; }
        public long PersonId { get; set; }
        public string PersonCode { get; set; }
        public string CentreCode { get; set; }
        public string UserType { get; set; }
        public short EmployeeDesignationMasterId { get; set; }
        public short GeneralDepartmentMasterId { get; set; }
        public bool IsEmployeeSmoker { get; set; }
        public long? ReportingEmployeeId { get; set; }
        public string PANCardNumber { get; set; }
        public string UANNumber { get; set; }
        public string PassportNumber { get; set; }
        public string AdharCardNumber { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankIFSCCode { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}



