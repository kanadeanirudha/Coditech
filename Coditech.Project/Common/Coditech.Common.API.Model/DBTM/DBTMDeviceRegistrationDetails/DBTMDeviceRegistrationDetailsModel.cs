namespace Coditech.Common.API.Model
{
    public class DBTMDeviceRegistrationDetailsModel : BaseModel
    {
        public long DBTMDeviceRegistrationDetailId { get; set; }
        public long DBTMDeviceMasterId { get; set; }
        public string UserType { get; set; }
        public long EntityId { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
