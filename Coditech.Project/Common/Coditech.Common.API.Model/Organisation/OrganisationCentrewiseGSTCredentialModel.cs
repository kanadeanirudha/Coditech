using Coditech.Common.API.Model;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Model
{
    public partial class OrganisationCentrewiseGSTCredentialModel : BaseModel
    {
        [Required]
        public short OrganisationCentrewiseGSTCredentialId { get; set; }

        [Required]
        public short OrganisationCentreMasterId { get; set; }

        [MaxLength(10)]
        [Required]
        public string Version { get; set; }

        [Required]
        public string Urls { get; set; }

        [MaxLength(200)]
        [Required]
        public string EInvoiceUserName { get; set; }

        [MaxLength(200)]
        [Required]
        public string EInvoicePassword { get; set; }

        [MaxLength(200)]
        [Required]
        public string AspId { get; set; }

        [MaxLength(200)]
        [Required]
        public string AspUserPassword { get; set; }

        public int QrCodeSize { get; set; }

        [MaxLength(200)]
        public string AuthToken { get; set; }

        [MaxLength(200)]
        public string TokenExpiry { get; set; }

        [MaxLength(200)]
        public string ClientId { get; set; }

        [Required]
        public bool IsLiveMode { get; set; }

        public string CentreName { get; set; }

        public string CentreCode { get; set; }
    }
}
