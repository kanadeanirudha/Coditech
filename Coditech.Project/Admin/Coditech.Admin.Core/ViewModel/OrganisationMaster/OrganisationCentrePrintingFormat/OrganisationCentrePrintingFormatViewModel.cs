using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrePrintingFormatViewModel : BaseViewModel
    {
        public int OrganisationCentrePrintingFormatId { get; set; }
        public int OrganisationCentreMasterId { get; set; }
        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }

        [MaxLength(100)]
        [Display(Name = " Printing Line 1 ")]
        public string PrintingLine1 { get; set; }

        [MaxLength(100)]
        [Display(Name = " Printing Line 2 ")]
        public string PrintingLine2 { get; set; }

        [MaxLength(100)]
        [Display(Name = " Printing Line 3 ")]
        public string PrintingLine3 { get; set; }

        [MaxLength(100)]
        [Display(Name = " Printing Line 4 ")]
        public string PrintingLine4 { get; set; }

        [MaxLength(100)]
        [Display(Name = "Printing Line below Logo")]
        public string PrintingLinebelowLogo { get; set; }
        [Display(Name = "Signature")]
        public long SignatureMediaId { get; set; }
        [Display(Name = "Logo")]
        public long LogoMediaId { get; set; }
       
        [Display(Name = "Centre Name")]
        public string CentreName { get; set; }
        public string LogoMediaPath { get; set; }
        public string LogoMediaFileName { get; set; }
        public string SignatureMediaPath { get; set; }
        public string SignatureMediaFileName { get; set; }
    }
}
