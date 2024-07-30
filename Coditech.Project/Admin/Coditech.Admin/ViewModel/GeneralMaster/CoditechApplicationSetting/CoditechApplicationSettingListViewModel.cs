using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class CoditechApplicationSettingListViewModel : BaseViewModel
    {
        public List<CoditechApplicationSettingViewModel> CoditechApplicationSettingList { get; set; }
        public CoditechApplicationSettingListViewModel()
        {
            CoditechApplicationSettingList = new List<CoditechApplicationSettingViewModel>();
        }
    }
}
