using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel

{
    public class GeneralNotificationListViewModel : BaseViewModel
    {
        public List<GeneralNotificationViewModel> GeneralNotificationList { get; set; }
        public GeneralNotificationListViewModel()
        {
            GeneralNotificationList = new List<GeneralNotificationViewModel>();
        }

    }
}
