namespace Coditech.API.Data
{
    public partial class GeneralTaxMaster
    {
        public short GeneralTaxMasterId { get; set; }
        public string TaxName { get; set; }
        public Nullable<decimal> TaxRate { get; set; }
        public Nullable<int> SalesGLAccount { get; set; }
        public Nullable<int> PurchasingGLAccount { get; set; }
        public Nullable<bool> IsCompoundTax { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsOtherState { get; set; }
    }
}

