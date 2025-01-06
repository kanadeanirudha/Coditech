using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPersonFollowUpViewModel : BaseViewModel
    {
        [Required]
        [Display(Name = "General Person Follow Up Id")]
        public long GeneralPersonFollowUpId { get; set; }

        [Required]
        [Display(Name = "Follow Taken Id")]
        public long FollowTakenId { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Follow Taken For")]
        public string FollowTakenFor { get; set; }

        [Required]
        [Display(Name = "Follow Up Types")]
        public int FollowupTypesEnumId { get; set; }

        [Required]
        [MaxLength(500)]
        [Display(Name = "Follow Up Comment")]
        public string FollowupComment { get; set; }

        [Required]
        [Display(Name = "Set Reminder")]
        public bool IsSetReminder { get; set; }

        [Display(Name = "Reminder Date")]
        public DateTime? ReminderDate { get; set; }

    }
}
