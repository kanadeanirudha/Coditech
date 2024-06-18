using Coditech.Common.API.Model;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Model
{
    public partial class OrganisationCentrewiseSmsSettingModel : BaseModel
    {
        [Required]
        public short OrganisationCentrewiseSmsSettingId { get; set; }

        [Required]
        public byte GeneralSmsProviderId { get; set; }

        [Required]
        [MaxLength(15)]
        public string CentreCode { get; set; }

        [MaxLength(50)]
        public string SmsPortalAccountId { get; set; }

        [MaxLength(50)]
        public string AuthToken { get; set; }

        [MaxLength(15)]
        public string FromMobileNumber { get; set; }

        [Required]
        public bool IsSMSSettingEnabled { get; set; }

        public string CentreName { get; set; }
    }
}
