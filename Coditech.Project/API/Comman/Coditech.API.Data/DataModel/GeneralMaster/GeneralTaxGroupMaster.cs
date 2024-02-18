using System.ComponentModel.DataAnnotations.Schema;

namespace Coditech.API.Data
{
    public partial class GeneralTaxGroupMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte GeneralTaxGroupMasterId { get; set; }
        public string TaxGroupName { get; set; }
        public bool IsOtherState { get; set; }
        public Nullable<decimal> TaxGroupRate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

