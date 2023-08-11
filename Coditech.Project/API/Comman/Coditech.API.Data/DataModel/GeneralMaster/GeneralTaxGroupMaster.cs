namespace Coditech.API.Data
{
    public partial class GeneralTaxGroupMaster
    {
        public byte GeneralTaxGroupMasterId { get; set; }
        public string TaxGroupName { get; set; }
        public decimal TaxGroupRate { get; set; }
        public List<string> GeneralTaxMasterIds { get; set; }
        public bool IsOtherState { get; set; }
    }
}

