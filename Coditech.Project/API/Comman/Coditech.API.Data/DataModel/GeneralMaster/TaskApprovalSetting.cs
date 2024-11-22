using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class TaskApprovalSetting
    {
        [Key]
        public int TaskApprovalSettingId { get; set; }
        public string CentreCode { get; set; }
        public short TaskMasterId { get; set; }
        public long EmployeeId { get; set; }
        public byte ApprovalSequenceNumber { get; set; }
        public bool IsFinalApproval { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

