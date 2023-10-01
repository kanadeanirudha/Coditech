using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralDesignationListViewModel : BaseViewModel
    {
        public List<GeneralDesignationViewModel> GeneralDesignationList { get; set; }

        public GeneralDesignationListViewModel()
        {
            GeneralDesignationList = new List<GeneralDesignationViewModel>();
        }
    }
}
