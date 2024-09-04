using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GazetteChapterPageDetails
    {
        [Key]
        public int GazetteChapterPageDetailId { get; set; }
        public int GazetteChapterId { get; set; }
        public string PageHeader { get; set; }
        public string PageFooter { get; set; }
        public string PageContent { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

    }
}

