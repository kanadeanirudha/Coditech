using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GazetteChaptersListViewModel : BaseViewModel
    {
        public List<GazetteChaptersViewModel> GazetteChaptersList { get; set; }
        public GazetteChaptersListViewModel()
        {
            GazetteChaptersList = new List<GazetteChaptersViewModel>();
        }
    }
}
