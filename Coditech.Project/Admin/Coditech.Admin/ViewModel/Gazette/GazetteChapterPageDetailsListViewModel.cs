using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GazetteChaptersPageDetailListViewModel : BaseViewModel
    {
        public List<GazetteChaptersPageDetailViewModel> GazetteChaptersPageDetailList { get; set; }
        public GazetteChaptersPageDetailListViewModel()
        {
            GazetteChaptersPageDetailList = new List<GazetteChaptersPageDetailViewModel>();
        }
    }
}
