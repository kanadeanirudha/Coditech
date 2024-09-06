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
            public DateTime FromDate { get; set; }
            [Required]
            public DateTime UptoDate {  get; set; } 
            public bool IsActive { get; set; }
    }
}
