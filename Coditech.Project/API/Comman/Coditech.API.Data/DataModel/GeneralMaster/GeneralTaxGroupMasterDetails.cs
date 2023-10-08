namespace Coditech.API.Data
{
    public partial class GeneralTaxGroupMasterDetails
    {
        public short GeneralTaxGroupMasterDetailsId { get; set; }
        public byte GeneralTaxGroupMasterId { get; set; }
        public short GeneralTaxMasterId { get; set; }
        public bool IsOtherState { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

