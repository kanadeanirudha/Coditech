using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class OrganisationCentrewiseWhatsAppSetting
    {
        [Key]
        public short OrganisationCentrewiseWhatsAppSettingId { get; set; }
        public string CentreCode { get; set; }
        public byte GeneralWhatsAppProviderId { get; set; }
        public string WhatsAppPortalAccountId { get; set; }
        public string AuthToken { get; set; }
        public string FromMobileNumber { get; set; }
        public bool IsWhatsAppSettingEnabled { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
