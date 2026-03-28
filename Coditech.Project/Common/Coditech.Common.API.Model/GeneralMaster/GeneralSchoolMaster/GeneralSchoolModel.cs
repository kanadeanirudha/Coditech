using System.ComponentModel.DataAnnotations;
namespace Coditech.Common.API.Model
{
    public class GeneralSchoolModel : BaseModel
    {
        public short GeneralSchoolMasterId { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCode { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public short GeneralCountryMasterId { get; set; }
        public short GeneralRegionMasterId { get; set; }
        public int GeneralCityMasterId { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string CityName { get; set; }
    }
}
