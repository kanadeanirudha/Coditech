using System.ComponentModel.DataAnnotations.Schema;

namespace Coditech.API.Data
{
    public partial class GeneralTaxGroupMaster : BaseDataModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte GeneralTaxGroupMasterId { get; set; }
        public string TaxGroupName { get; set; }
        public bool IsOtherState { get; set; }
        public Nullable<decimal> TaxGroupRate { get; set; }
    }
}

