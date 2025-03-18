using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralRunningNumbersViewModel : BaseViewModel
    {
        [Required]
        public long GeneralRunningNumberId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Key Field Enum")]
        public int KeyFieldEnumId { get; set; }

        [Display(Name = "Financial Year")]
        public Nullable<int> GeneralFinancialYearId { get; set; }

        [MaxLength(15)]
        [Required]
        [Display(Name = "Centre")]
        public string CentreCode { get; set; }
        public string SelectedCentreCode { get; set; }


        [MaxLength(200)]
        [Required]
        [Display(Name = "Running Number Format")]
        public string DisplayFormat { get; set; }

        [Required]
        [Display(Name = "Is Sequence Reset")]
        public bool IsSequenceReset { get; set; }

        [MaxLength(10)]
        [Required]
        public string Separator { get; set; }

        [MaxLength(20)]
        [Required]
        public string Prefix { get; set; }

        [Required]
        [Display(Name = "Is Back Dated")]
        public bool IsBackDated { get; set; }

        [MaxLength(20)]
        [Display(Name = "Back Dated Prefix")]
        public string BackDatedPrefix { get; set; }

        [Required]
        [Display(Name = "Start Sequence")]
        public long StartSequence { get; set; }

        [Required]
        [Display(Name = "Current Sequnce")]
        public long CurrentSequnce { get; set; }

        [Required]
        [Display(Name = "Is Row Lock")]
        public bool IsRowLock { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
