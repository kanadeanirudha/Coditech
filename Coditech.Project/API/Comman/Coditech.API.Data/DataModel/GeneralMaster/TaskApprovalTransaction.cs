using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class TaskApprovalTransaction
    {
        [Key]
        public long TaskApprovalTransactionId { get; set; }
        public int TaskApprovalSettingId { get; set; }
        public int TaskApprovalStatusEnumId { get; set; }
        public bool IsCurrentStatus { get; set; }
        public long TablePrimaryColumnId { get; set; }
        public string Comments { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

