using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralEnumaratorListViewModel : BaseViewModel
    {
        public List<GeneralEnumaratorViewModel> GeneralEnumaratorList { get; set; }
        public GeneralEnumaratorListViewModel()
        {
            GeneralEnumaratorList = new List<GeneralEnumaratorViewModel>();
        }
    }
}
