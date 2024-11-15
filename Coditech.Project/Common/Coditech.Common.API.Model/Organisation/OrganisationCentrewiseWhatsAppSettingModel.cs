using Coditech.Common.API.Model;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Model
{
    public class OrganisationCentrewiseWhatsAppSettingModel : BaseModel
    {
        [Required]
        public int OrganisationCentrewiseWhatsAppSettingId { get; set; }
        public int OrganisationCentreMasterId { get; set; }

        [Required]
        public byte GeneralWhatsAppProviderId { get; set; }

        [Required]
        [MaxLength(15)]
        public string CentreCode { get; set; }

        [MaxLength(50)]
        public string WhatsAppPortalAccountId { get; set; }

        [MaxLength(50)]
        public string AuthToken { get; set; }

        [MaxLength(15)]
        public string FromMobileNumber { get; set; }

        [Required]
        public bool IsWhatsAppSettingEnabled { get; set; }
        public string CentreName { get; set; }
    }
}
