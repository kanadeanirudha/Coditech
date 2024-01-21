using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GymMemberFollowUp
    {
        [Key]
        public long GymMemberFollowUpId { get; set; }
        public int GymFollowupTypesEnumId { get; set; }
        public int GymMemberDetailId { get; set; }
        public string FollowupComment { get; set; }
        public bool IsSetReminder { get; set; }
        public DateTime? ReminderDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

