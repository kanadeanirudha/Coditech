using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class OrganisationCentrewiseSmsSetting
    {
        [Key]
        public short OrganisationCentrewiseSmsSettingId { get; set; }
        public string CentreCode { get; set; }
        public byte GeneralSmsProviderId { get; set; }
        public string SmsPortalAccountId { get; set; }
        public string AuthToken { get; set; }
        public string FromMobileNumber { get; set; }
        public bool IsSMSSettingEnabled { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
