namespace Coditech.Common.API.Model
{
    public class TaskApprovalSettingModel : BaseModel
    {
        public int TaskApprovalSettingId { get; set; }
        public string CentreCode { get; set; }
        public short TaskMasterId { get; set; }
        public long EmployeeId { get; set; }       
        public string TaskCode { get; set; }
        public string TaskDescription { get; set; }
        public string CentreName { get; set; }
        public byte CountNumber { get; set; }
        public List<EmployeeMasterModel> EmployeeList { get; set; }

    }
}
