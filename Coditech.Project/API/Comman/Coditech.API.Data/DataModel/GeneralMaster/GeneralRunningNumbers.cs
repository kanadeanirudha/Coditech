using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralRunningNumbers
    {
        [Key]
        public long GeneralRunningNumberId { get; set; }
        public string Description { get; set; }
        public int KeyFieldEnumId { get; set; }
        public int GeneralFinancialYearId { get; set; }
        public string CentreCode { get; set; }
        public string DisplayFormat { get; set; }
        public bool IsSequenceReset { get; set; }
        public string Separator { get; set; }
        public string Prefix { get; set; }
        public bool IsBackDated { get; set; }
        public string BackDatedPrefix { get; set; }
        public long StartSequence { get; set; }
        public long CurrentSequnce { get; set; }
        public bool IsRowLock { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

