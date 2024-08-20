namespace Coditech.Common.API.Model
{
    public class GazetteChaptersModel : BaseModel
    {
        public int GazetteChapterId { get; set; } 
        public short GeneralDistrictMasterId { get; set; }
        public string ChapterName { get; set; }
        public string ChapterNumber { get; set; }
        public short GeneralRegionMasterId { get; set; }
        public short GeneralCountryMasterId { get; set; }
    }
}
