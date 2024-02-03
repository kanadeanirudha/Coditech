using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralRunningNumbersModel : BaseModel
    {
        [Required]
        public long GeneralRunningNumberId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Description { get; set; }

        [Required]
        public int KeyFieldEnumId { get; set; }

        public int? GeneralFinancialYearId { get; set; }

        [MaxLength(15)]
        [Required]
        public string CentreCode { get; set; }

        [MaxLength(200)]
        [Required]
        public string DisplayFormat { get; set; }

        [Required]
        public bool IsSequenceReset { get; set; }

        [MaxLength(10)]
        [Required]
        public string Separator { get; set; }

        [MaxLength(20)]
        [Required]
        public string Prefix { get; set; }

        [Required]
        public bool IsBackDated { get; set; }

        [MaxLength(20)]
        public string BackDatedPrefix { get; set; }

        [Required]
        public long StartSequence { get; set; }

        [Required]
        public long CurrentSequnce { get; set; }

        [Required]
        public bool IsRowLock { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
