using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class OrganisationCentrewiseWhatsUpSetting
    {
        [Key]
        public short OrganisationCentrewiseWhatsUpSettingId { get; set; }
        public string CentreCode { get; set; }
        public byte GeneralWhatsUpProviderId { get; set; }
        public string WhatsUpPortalAccountId { get; set; }
        public string AuthToken { get; set; }
        public string FromMobileNumber { get; set; }
        public bool IsWhatsUpSettingEnabled { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
