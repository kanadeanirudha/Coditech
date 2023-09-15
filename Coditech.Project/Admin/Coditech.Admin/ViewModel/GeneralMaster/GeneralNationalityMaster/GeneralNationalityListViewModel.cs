using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralNationalityListViewModel : BaseViewModel
    {
        public List<GeneralNationalityViewModel> GeneralNationalityList { get; set; }

        public GeneralNationalityListViewModel()
        {
            GeneralNationalityList = new List<GeneralNationalityViewModel>();
        }
    }
}
