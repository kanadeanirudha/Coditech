using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralRegionViewModel : BaseViewModel
    {
        [Required]
        public short GeneralRegionMasterId { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "State Name")]
        public string RegionName { get; set; }

        [Required]
        [Display(Name = "Country Name")]
        public int GeneralCountryMasterId { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }

        [Display(Name = "Default Flag")]
        public bool DefaultFlag { get; set; }

        [Required]
        public bool IsUserDefined { get; set; }

        [Display(Name = "Tin Number")]
        public short? TinNumber { get; set; }
    }
}
