namespace Coditech.API.Data
{
    public partial class GeneralRegionMaster
    {
        public short GeneralRegionMasterId { get; set; }
        public string RegionName { get; set; }
        public int GeneralCountryMasterId { get; set; }
        public string ShortName { get; set; }
        public bool DefaultFlag { get; set; }
        public bool IsUserDefined { get; set; }
        public Nullable<short> TinNumber { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

