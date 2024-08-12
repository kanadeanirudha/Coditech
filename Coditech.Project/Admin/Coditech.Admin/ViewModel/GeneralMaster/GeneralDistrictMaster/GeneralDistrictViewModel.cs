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
        public string DistrictName { get; set; }
        public short GeneralRegionMasterId { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "State Name")]
        public string RegionName { get; set; }

        [Required]
        [Display(Name = "Country Name")]
        public int GeneralCountryMasterId { get; set; }
    }
}
