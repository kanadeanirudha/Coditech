using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralTrainerListViewModel : BaseViewModel
    {
        public List<GeneralTrainerViewModel> GeneralTrainerList { get; set; }
        public GeneralTrainerListViewModel()
        {
            GeneralTrainerList = new List<GeneralTrainerViewModel>();
        }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public int SelectedDepartmentId { get; set; }
    }
}
