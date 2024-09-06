using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Numerics;

namespace Coditech.Common.API.Model
{
    public class GeneralNotificationModel : BaseModel
    {
        public GeneralNotificationModel()
        {


        }
        public long GeneralNotificationId { get; set; }   
        public string NotificationDetails { get; set; } 
        public DateTime FromDate { get; set; }
        public DateTime UptoDate { get; set; }
        public bool IsActive { get; set; }  



    }
}
