using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GazetteChaptersViewModel : BaseViewModel
    {
        public int GazetteChapterId { get; set; }

        [Required]
        [Display(Name = "District")]
        public short GeneralDistrictMasterId { get; set; }

        [MaxLength(500)]
        [Required]
        [Display(Name = "Chapter Name")]
        public string ChapterName { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "Chapter Number")]
        public string ChapterNumber { get; set; }

        [MaxLength(200)]
        [Display(Name = "District Name")]
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
