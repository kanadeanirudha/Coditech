namespace Coditech.API.Data
{
    public partial class GeneralTaxGroupMasterDetails
    {
        public short GeneralTaxGroupMasterDetailsId { get; set; }
        public byte GeneralTaxGroupMasterId { get; set; }
        public short GeneralTaxMasterId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

