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
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string RegionName { get; set; }
        public string CountryName { get; set; }

        [Required]
        [Display(Name = "Country Name")]
        public short GeneralCountryMasterId { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Short Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string ShortName { get; set; }

        [Display(Name = "Default Flag")]
        public bool DefaultFlag { get; set; }

        [Required]
        public bool IsUserDefined { get; set; }

        [Display(Name = "Tin Number")]
        public short? TinNumber { get; set; }
    }
}
