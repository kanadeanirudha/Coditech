using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralDistrictViewModel : BaseViewModel
    {
        public short GeneralDistrictMasterId { get; set; }
        [MaxLength(200)]
        [Required]
        [Display(Name = "District Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string DistrictName { get; set; }

        [Required]
        [Display(Name = "Region")]
        public short GeneralRegionMasterId { get; set; }

        public string RegionName { get; set; }

        [Required]
        [Display(Name = "Country")]
        public short GeneralCountryMasterId { get; set; }
    }
}
