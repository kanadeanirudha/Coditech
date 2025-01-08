using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class AccSetupMasterViewModel : BaseViewModel
    {
        public short AccSetupMasterId { get; set; }
        [Required]
        [Display(Name = "Fiscal Day")]
        [RegularExpression(@"^(0?[1-9]|[1-2][0-9]|3[0-1])$", ErrorMessage = "Enter Date From 1 to 31.")]
        public byte FiscalYearDay { get; set; }
        [Display(Name = "Fiscal Month")]
        [RegularExpression(@"^(0?[1-9]|1[0-2])$", ErrorMessage = "Enter Month From 1 to 12.")]
        public byte FiscalYearMonth { get; set; }
        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

    }
}
