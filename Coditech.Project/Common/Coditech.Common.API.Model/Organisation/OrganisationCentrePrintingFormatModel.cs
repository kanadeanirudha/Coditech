using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class OrganisationCentrePrintingFormatModel : BaseModel
    {
        [Required]
        public int OrganisationCentrePrintingFormatId { get; set; }

        [Required]
        public int OrganisationCentreMasterId { get; set; }

        [MaxLength(100)]
        [Display(Name = " PrintingLine 1 ")]
        public string PrintingLine1 { get; set; }

        [MaxLength(100)]
        [Display(Name = " PrintingLine 2 ")]
        public string PrintingLine2 { get; set; }

        [MaxLength(100)]
        [Display(Name = " PrintingLine 3 ")]
        public string PrintingLine3 { get; set; }

        [MaxLength(100)]
        [Display(Name = " PrintingLine 4 ")]
        public string PrintingLine4 { get; set; }
        public long SignatureMediaId { get; set; }
        public long LogoMediaId { get; set; }

        [MaxLength(100)]
        public string PrintingLinebelowLogo { get; set; }
        public string CentreName { get; set; }
        public string CentreCode { get; set; }
        public string LogoMediaPath { get; set; }
        public string LogoMediaFileName { get; set; }
        public string SignatureMediaPath { get; set; }
        public string SignatureMediaFileName { get; set; }
    }
}
