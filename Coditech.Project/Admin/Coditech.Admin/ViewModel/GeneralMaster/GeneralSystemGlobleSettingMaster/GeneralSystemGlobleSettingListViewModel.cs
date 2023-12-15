using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralSystemGlobleSettingListViewModel : BaseViewModel
    {
        public List<GeneralSystemGlobleSettingViewModel> GeneralSystemGlobleSettingList { get; set; }
        public GeneralSystemGlobleSettingListViewModel()
        {
            GeneralSystemGlobleSettingList = new List<GeneralSystemGlobleSettingViewModel>();
        }
    }
}
