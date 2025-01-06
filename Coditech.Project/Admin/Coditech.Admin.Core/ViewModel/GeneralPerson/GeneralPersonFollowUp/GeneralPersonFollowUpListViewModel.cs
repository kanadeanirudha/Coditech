using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPersonFollowUpListViewModel : BaseViewModel
    {
        public List<GeneralPersonFollowUpViewModel> GeneralPersonFollowUpList { get; set; }
        public GeneralPersonFollowUpListViewModel()
        {
            GeneralPersonFollowUpList = new List<GeneralPersonFollowUpViewModel>();
        }
    }
}
