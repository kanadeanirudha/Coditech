using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseSmtpSettingViewModel : BaseViewModel
    {
        [Required]
        public int OrganisationCentrewiseSmtpSettingId { get; set; }

        [Required]
        public int OrganisationCentreMasterId { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "SMTP Server")]
        public string ServerName { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "SMTP Server User Name")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        [Display(Name = "SMTP Server Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "SMTP Port")]
        public int Port { get; set; }

        [Required]
        [Display(Name = "Is Enable SSL")]
        public bool IsEnableSsl { get; set; }

        [MaxLength(256)]
        [Display(Name = "From Display Name")]
        public string FromDisplayName { get; set; }

        [EmailAddress]
        [MaxLength(256)]
        [Display(Name = "From Email Address")]
        public string FromEmailAddress { get; set; }

        [EmailAddress]
        [MaxLength(256)]
        [Display(Name = "Bcc Email Address")]
        public string BccEmailAddress { get; set; }

        [Required]
        [Display(Name = "Disable All Emails")]
        public bool DisableAllEmails { get; set; }

        [Display(Name = "Centre Name")]
        public string CentreName { get; set; }
    }
}
