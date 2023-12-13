using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralEnumaratorListViewModel : BaseViewModel
    {
        public List<GeneralEnumaratorListViewModel> GeneralEnumaratorList { get; set; }
        public GeneralEnumaratorListViewModel()
        {
            GeneralEnumaratorList = new List<GeneralEnumaratorListViewModel>();
        }
    }
}
