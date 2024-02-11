using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralPersonFollowUp
    {

        public long GeneralPersonFollowUpId { get; set; }
        public long FollowTakenId { get; set; }
        public string FollowTakenFor { get; set; }
        public int FollowupTypesEnumId { get; set; }
        public string FollowupComment { get; set; }
        public bool IsSetReminder { get; set; }
        public DateTime? ReminderDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

