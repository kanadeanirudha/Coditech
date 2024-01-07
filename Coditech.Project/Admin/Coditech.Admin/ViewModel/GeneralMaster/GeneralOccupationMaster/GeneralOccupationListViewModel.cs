using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralOccupationListViewModel : BaseViewModel
    {
        public List<GeneralOccupationViewModel> GeneralOccupationList { get; set; }
        public GeneralOccupationListViewModel()
        {
            GeneralOccupationList = new List<GeneralOccupationViewModel>();
        }
    }
}
