using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GazetteChaptersPageDetailViewModel : BaseViewModel
    {
        public int GazetteChapterPageDetailId { get; set; }
        public int GazetteChapterId { get; set; }
        
        [Display(Name = "Chapter Name")]
        public string ChapterName { get; set; }

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

        [Display(Name = "District Name")]
        public string DistrictName { get; set; }
    }
}
