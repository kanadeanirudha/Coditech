using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralCityViewModel : BaseViewModel
    {
        public int GeneralCityMasterId { get; set; }
        [Required]
        public string CityName { get; set; }
        [Required]
        [Display(Name = "Is Default")]
        public bool DefaultFlag { get; set; }
        //[Required]
        public int GeneralRegionMasterId { get; set; }
        public string RegionName { get; set; }
        public Int16? TinNumber { get; set; }
        public bool IsUserDefined { get; set; }
        public List<GeneralCityModel> AllCityList { get; internal set; }
    }
}
