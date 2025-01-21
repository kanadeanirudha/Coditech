using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class DBTMActivitiesViewModel : BaseViewModel
    {
        
        public string TestName { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(100)]
        [Required]
        public string DeviceSerialCode { get; set; }
        public long DBTMDeviceDataId { get; set; }
    }
}
