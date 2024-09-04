namespace Coditech.Common.API.Model
{
    public class GazetteChaptersPageDetailListModel : BaseListModel
    {
        public List<GazetteChaptersPageDetailModel> GazetteChaptersPageDetailList { get; set; }
        public GazetteChaptersPageDetailListModel()
        {
            GazetteChaptersPageDetailList = new List<GazetteChaptersPageDetailModel>();
        }

    }
}
