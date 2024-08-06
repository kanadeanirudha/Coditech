using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class MediaSettingMasterListViewModel : BaseViewModel
    {
        public List<MediaSettingMasterViewModel> MediaSettingMasterList { get; set; }
        public MediaSettingMasterListViewModel()
        {
            MediaSettingMasterList = new List<MediaSettingMasterViewModel>();
        }
    }
}
