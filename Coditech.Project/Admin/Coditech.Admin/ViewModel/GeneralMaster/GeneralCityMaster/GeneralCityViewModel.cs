using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralCityViewModel : BaseViewModel
    {
        [Required]
        public int GeneralCityMasterId { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "Description")]
        public string CityName { get; set; }

        [Required]
        [Display(Name = "Is Default")]
        public bool DefaultFlag { get; set; }

        [Required]
        [Display(Name = "State")]
        public int GeneralRegionMasterId { get; set; }

        public string RegionName { get; set; }

        public string CountryCode { get; set; }

        public Int16? TinNumber { get; set; }

        [Required]
        public bool IsUserDefined { get; set; }

        [Display(Name = "Country")]
        public string GeneralCountryMasterId { get; set; }
        public object CountryList { get; internal set; }
    }
}
