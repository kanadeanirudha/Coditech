namespace Coditech.API.Data
{
    public partial class GeneralCityMaster
    {
        public int GeneralCityMasterId { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public Int16? TinNumber { get; set; }
        public bool DefaultFlag { get; set; }
        public int GeneralRegionMasterId { get; set; }
        public bool IsUserDefined { get; set; }
    }
}

