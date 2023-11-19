﻿using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.ViewModel
{
    public partial class OrganisationCentrePrintingFormatViewModel : BaseViewModel
    {
        public short OrganisationCentrePrintingFormatId { get; set; }
        public short OrganisationCentreMasterId { get; set; }
        [MaxLength(15)]
        [Required]
        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }

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

        public byte[] Logo { get; set; }

        [MaxLength(50)]
        public string LogoType { get; set; }

        [MaxLength(50)]
        public string LogoFilename { get; set; }

        [MaxLength(50)]
        public string LogoFileWidth { get; set; }

        [MaxLength(50)]
        public string LogoFileHeight { get; set; }

        [MaxLength(50)]
        public string LogoFileSize { get; set; }

        [MaxLength(100)]
        public string PrintingLinebelowLogo { get; set; }

        [Display(Name = "Centre Name")]
        public string CentreName { get; set; }

    }
}
