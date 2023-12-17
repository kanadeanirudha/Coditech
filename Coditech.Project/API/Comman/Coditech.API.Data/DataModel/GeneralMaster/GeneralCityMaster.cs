namespace Coditech.API.Data
{
    public partial class GeneralCityMaster : BaseDataModel
    {
        public int GeneralCityMasterId { get; set; }
        public string CityName { get; set; }
        public bool DefaultFlag { get; set; }
        public short GeneralRegionMasterId { get; set; }
        public bool IsUserDefined { get; set; }
    }
}

