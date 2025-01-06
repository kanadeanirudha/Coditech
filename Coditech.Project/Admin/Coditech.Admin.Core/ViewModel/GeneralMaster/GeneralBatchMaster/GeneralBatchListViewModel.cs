using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralBatchListViewModel : BaseViewModel
    {
        public List<GeneralBatchViewModel> GeneralBatchList { get; set; }
        public GeneralBatchListViewModel()
        {
            GeneralBatchList = new List<GeneralBatchViewModel>();
        }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public long DBTMBatchActivityId { get; set; }
    }
}
