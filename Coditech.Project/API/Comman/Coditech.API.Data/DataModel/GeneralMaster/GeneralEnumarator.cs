namespace Coditech.API.Data
{
    public partial class GeneralEnumarator
    {
        public int GeneralEnumaratorId { get; set; }
        public int GeneralEnumaratorGroupId { get; set; }
        public string EnumName { get; set; }
        public string EnumDisplayText { get; set; }
        public string EnumValue { get; set; }
        public short SequenceNumber { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}




