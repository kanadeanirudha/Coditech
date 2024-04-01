namespace Coditech.Common.API.Model
{
    public class EmployeeServiceModel : BaseModel
    {
        public EmployeeServiceModel()
        {

        }
        public long EmployeeServiceId { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public short EmployeeDesignationMasterId { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime PromotionDemotionDate { get; set; }
        public int EmployeeStageEnumId { get; set; }
        public DateTime DateOfLeaving { get; set; }
        public bool IsCurrentPosition { get; set; }
        public string SalaryGradeCode { get; set; }
        public string PayScale { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public string Remark { get; set; }        
    }
}

