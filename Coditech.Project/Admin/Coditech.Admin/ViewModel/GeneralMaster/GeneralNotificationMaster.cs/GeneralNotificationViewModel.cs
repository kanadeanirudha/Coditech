using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralNotificationViewModel : BaseViewModel
    {
        [Required]
        [Display(Name = "GeneralNoticationId")]
        public short GeneralNotificationId { get; set; }

        [Required]
        [Display(Name = "Notification Details")]
        public string NotificationDetails { get; set; }
        [Required]
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }
        [Required]
        [Display(Name = "Upto Date")]
        public DateTime UptoDate { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
