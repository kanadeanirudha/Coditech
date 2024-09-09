namespace Coditech.Common.API.Model
{
    public class GazetteChaptersPageDetailModel : BaseModel
    {
        public int GazetteChapterPageDetailId { get; set; } 
        public int GazetteChapterId { get; set; }
        public string ChapterNumber { get; set; }
        public string ChapterName { get; set; }
        public string DistrictName { get; set; }
        public string PageHeader { get; set; }
        public string PageFooter { get; set; }
        public string PageContent { get; set; }
        public short GeneralRegionMasterId { get; set; }
        public short GeneralCountryMasterId { get; set; }
        public short GeneralDistrictMasterId { get; set; }
    }
}
