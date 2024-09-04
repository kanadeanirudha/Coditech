namespace Coditech.Common.API.Model
{
    public class GazetteChaptersListModel : BaseListModel
    {
        public List<GazetteChaptersModel> GazetteChaptersList { get; set; }
        public GazetteChaptersListModel()
        {
            GazetteChaptersList = new List<GazetteChaptersModel>();
        }

    }
}
