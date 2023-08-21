using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralTaxGroupMasterModel : BaseModel
    {
        public GeneralTaxGroupMasterModel()
        {

        }
        public byte GeneralTaxGroupMasterId { get; set; }
        public string TaxGroupName { get; set; }
        public decimal TaxGroupRate { get; set; }
        public List<string> GeneralTaxMasterIds { get; set; }
        public bool IsOtherState { get; set; }
    }
}
