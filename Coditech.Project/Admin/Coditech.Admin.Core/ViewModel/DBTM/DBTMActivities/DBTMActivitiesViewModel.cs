using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class DBTMActivitiesViewModel : BaseViewModel
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TestName { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(100)]
        [Required]
        public string DeviceSerialCode { get; set; }
    }
}
