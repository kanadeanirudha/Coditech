using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public partial class AccSetupTransactionTypeListViewModel : BaseViewModel
    {
        public List<AccSetupTransactionTypeViewModel> AccSetupTransactionTypeList { get; set; }
        public AccSetupTransactionTypeListViewModel()
        {
            AccSetupTransactionTypeList = new List<AccSetupTransactionTypeViewModel>();
        }
    }
}
