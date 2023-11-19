using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralRegionModel : BaseModel
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

        [Required]
        public bool DefaultFlag { get; set; }

        [Required]
        public bool IsUserDefined { get; set; }

        public short? TinNumber { get; set; }
    }
}
