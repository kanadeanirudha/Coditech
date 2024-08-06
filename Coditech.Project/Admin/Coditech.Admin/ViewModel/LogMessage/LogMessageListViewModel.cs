using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class LogMessageListViewModel : BaseViewModel
    {
        public List<LogMessageViewModel> LogMessageList { get; set; }
        public LogMessageListViewModel()
        {
            LogMessageList = new List<LogMessageViewModel>();
        }
    }
}
