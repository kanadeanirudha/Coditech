using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class DBTMDeviceRegistrationDetailsViewModel : BaseViewModel
    {
        public long DBTMDeviceRegistrationDetailId { get; set; }
        [Display(Name = "Device")]
        public long DBTMDeviceMasterId { get; set; }
        public string UserType { get; set; }
        public long EntityId { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
