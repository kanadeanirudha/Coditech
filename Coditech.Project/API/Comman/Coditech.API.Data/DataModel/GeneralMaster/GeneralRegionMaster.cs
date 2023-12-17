namespace Coditech.API.Data
{
    public partial class GeneralRegionMaster : BaseDataModel
    {
        public short GeneralRegionMasterId { get; set; }
        public string RegionName { get; set; }
        public short GeneralCountryMasterId { get; set; }
        public string ShortName { get; set; }
        public bool DefaultFlag { get; set; }
        public bool IsUserDefined { get; set; }
        public Nullable<short> TinNumber { get; set; }
    }
}

