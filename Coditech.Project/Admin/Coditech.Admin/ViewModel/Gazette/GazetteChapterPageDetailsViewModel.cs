using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GazetteChaptersPageDetailViewModel : BaseViewModel
    {
        public int GazetteChapterPageDetailId { get; set; }
        public int GazetteChapterId { get; set; }
        
        [MaxLength(500)]
        [Required]
        [Display(Name = "Chapter Name")]
        public string ChapterName { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "Chapter Number")]
        public string ChapterNumber { get; set; }

        [MaxLength(200)]
        [Required]
        [Display(Name = "Page Header")]
        public string PageHeader { get; set; }

        [MaxLength(200)]
        [Required]
        [Display(Name = "Page Footer")]
        public string PageFooter { get; set; }

        [Required]
        [Display(Name = "Page Content")]
        public string PageContent { get; set; }

        [Required]
        [Display(Name = "District")]
        public short GeneralDistrictMasterId { get; set; }

        [Required]
        [Display(Name = "Region")]
        public short GeneralRegionMasterId { get; set; }

        [Required]
        [Display(Name = "Country")]
        public short GeneralCountryMasterId { get; set; }

        [MaxLength(200)]
        [Display(Name = "District Name")]
        public string DistrictName { get; set; }
    }
}
