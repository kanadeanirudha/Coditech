namespace Coditech.API.Data
{
    public partial class GeneralTaxGroupMaster
    {
        public byte GeneralTaxGroupMasterId { get; set; }
        public string TaxGroupName { get; set; }
        public Nullable<decimal> TaxGroupRate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

