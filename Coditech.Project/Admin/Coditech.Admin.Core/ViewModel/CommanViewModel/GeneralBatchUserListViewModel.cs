using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralBatchUserListViewModel : BaseViewModel
    {
        public List<GeneralBatchUserViewModel> GeneralBatchUserList { get; set; }
        public GeneralBatchUserListViewModel()
        {
            GeneralBatchUserList = new List<GeneralBatchUserViewModel>();
        }
        public int GeneralBatchMasterId { get; set; }
        public string BatchName { get; set; }
        public string SelectedParameter1 { get; set; }
        public string SelectedParameter2 { get; set; }
    }
}
