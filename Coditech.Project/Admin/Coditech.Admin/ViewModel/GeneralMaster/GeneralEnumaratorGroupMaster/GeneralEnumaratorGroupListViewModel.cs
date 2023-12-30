using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralEnumaratorGroupListViewModel : BaseViewModel
    {
        public List<GeneralEnumaratorGroupViewModel> GeneralEnumaratorGroupList { get; set; }
        public GeneralEnumaratorGroupListViewModel()
        {
            GeneralEnumaratorGroupList = new List<GeneralEnumaratorGroupViewModel>();
        }
    }
}
