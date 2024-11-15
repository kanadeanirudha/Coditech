using Coditech.Common.API.Model;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Model
{
    public partial class OrganisationCentrewiseSmtpSettingModel : BaseModel
    {
        [Required]
        public int OrganisationCentrewiseSmtpSettingId { get; set; }

        [Required]
        public int OrganisationCentreMasterId { get; set; }

        [Required]
        [MaxLength(15)]
        public string CentreCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string ServerName { get; set; }

        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Password { get; set; }

        [Required]
        public int Port { get; set; }

        [Required]
        public bool IsEnableSsl { get; set; }

        [MaxLength(256)]
        public string FromDisplayName { get; set; }

        [MaxLength(256)]
        public string FromEmailAddress { get; set; }

        [MaxLength(256)]
         public string BccEmailAddress { get; set; }

        [Required]
        public bool DisableAllEmails { get; set; }

        public string CentreName { get; set; }
    }
}
