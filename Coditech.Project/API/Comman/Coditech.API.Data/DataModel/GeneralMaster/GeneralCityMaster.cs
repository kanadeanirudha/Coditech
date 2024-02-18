namespace Coditech.API.Data
{
    public partial class GeneralCityMaster
    {
        public int GeneralCityMasterId { get; set; }
        public string CityName { get; set; }
        public bool DefaultFlag { get; set; }
        public short GeneralRegionMasterId { get; set; }
        public bool IsUserDefined { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

