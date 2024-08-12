namespace Coditech.API.Data
{
    public partial class GeneralDistrictMaster
    {
        public short GeneralDistrictMasterId { get; set; }
        public string DistrictName { get; set; }
        public short GeneralRegionMasterId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

