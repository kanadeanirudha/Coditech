using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralPersonFollowUpModel : BaseModel
    {
        [Required]
        public long GeneralPersonFollowUpId { get; set; }

        [Required]
        public long FollowTakenId { get; set; }

        [Required]
        [MaxLength(200)]
        public string FollowTakenFor { get; set; }

        [Required]
        public int FollowupTypesEnumId { get; set; }

        [Required]
        [MaxLength(500)]
        public string FollowupComment { get; set; }

        [Required]
        public bool IsSetReminder { get; set; }

        public DateTime? ReminderDate { get; set; }
    }
}