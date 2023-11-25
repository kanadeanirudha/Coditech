using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralCityModel : BaseModel
    {
        [Required]
        public int GeneralCityMasterId { get; set; }

        [MaxLength(100)]
        [Required]
        public string CityName { get; set; }

        public string RegionName { get; set; }

        [Required]
        [Display(Name = "Is Default")]
        public bool DefaultFlag { get; set; }

        [Required]
        [Display(Name = "State")]
        public int GeneralRegionMasterId { get; set; }

        [Required]
        public bool IsUserDefined { get; set; }
        public short GeneralCountryMasterId { get; set; }
    }
}
