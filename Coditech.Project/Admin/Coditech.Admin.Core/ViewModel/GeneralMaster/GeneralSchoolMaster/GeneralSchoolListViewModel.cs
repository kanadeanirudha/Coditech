using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class GeneralSchoolListViewModel : BaseViewModel
    {
        public List<GeneralSchoolViewModel> GeneralSchoolList { get; set; }
        public GeneralSchoolListViewModel()
        {
            GeneralSchoolList = new List<GeneralSchoolViewModel>();
        }
    }
}
