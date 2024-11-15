using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseGSTCredentialViewModel : BaseViewModel
    {
        [Required]
        public int OrganisationCentrewiseGSTCredentialId { get; set; }

        [Required]
        public int OrganisationCentreMasterId { get; set; }

        [MaxLength(10)]
        [Required]
        public string Version { get; set; }

        [Required]
        [Display(Name = "Url")]
        public string Urls { get; set; }

        [MaxLength(200)]
        [Required]
        [Display(Name = "E Invoice UserName")]
        public string EInvoiceUserName { get; set; }

        [MaxLength(200)]
        [Required]
        [Display(Name = "E Invoice Password")]
        public string EInvoicePassword { get; set; }

        [MaxLength(200)]
        [Required]
        [Display(Name = "Asp Id")]
        public string AspId { get; set; }

        [MaxLength(200)]
        [Required]
        [Display(Name = "Asp User Password")]
        public string AspUserPassword { get; set; }

        [Display(Name = "Qr Code Size")]
        public int QrCodeSize { get; set; }

        [MaxLength(200)]
        [Display(Name = "Authentication")]
        public string AuthToken { get; set; }

        [MaxLength(200)]
        [Display(Name = "Token Valid For")]
        public string TokenExpiry { get; set; }

        [MaxLength(200)]
        [Display(Name = "Client Id")]
        public string ClientId { get; set; }

        [Required]
        [Display(Name = "Is Live Mode")]
        public bool IsLiveMode { get; set; }

        [Display(Name = "Centre Name")]
        public string CentreName { get; set; }

        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }
    }
}
