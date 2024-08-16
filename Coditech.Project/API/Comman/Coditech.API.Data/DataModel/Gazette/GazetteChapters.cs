using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GazetteChapters
    {
        [Key]
        public int GazetteChapterId { get; set; }
        public short GeneralDistrictMasterId { get; set; }
        public string ChapterName { get; set; }
        public string ChapterNumber { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

    }
}

