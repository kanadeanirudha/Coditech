using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralTaxMasterModel : BaseModel
    {
        public GeneralTaxMasterModel()
        {

        }
        public short GeneralTaxMasterId { get; set; }
        [MaxLength(50)]
        [Required]
        public string TaxName { get; set; }
        [Required]
        public decimal TaxRate { get; set; }
        public int? SalesGLAccount { get; set; }
        public int? PurchasingGLAccount { get; set; }
        public bool IsCompoundTax { get; set; }
        public bool IsOtherState { get; set; }
    }
}
