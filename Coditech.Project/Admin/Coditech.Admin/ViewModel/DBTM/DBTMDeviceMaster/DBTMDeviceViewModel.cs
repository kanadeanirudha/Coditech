using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class DBTMDeviceViewModel : BaseViewModel
    {
        public long DBTMDeviceMasterId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceSerialCode { get; set; }
        public string Description { get; set; }
        public string ManufacturedBy { get; set; }
        public int Status { get; set; }
        public bool IsMasterDevice { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegistrationDate { get; set; }
        public short WarrantyExpirationPeriodInMonth { get; set; }
        public string AdditionalFeatures { get; set; }
    }
}
