using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralNotification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long GeneralNotificationId { get; set; }
        public string NotificationDetails { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime UptoDate { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

