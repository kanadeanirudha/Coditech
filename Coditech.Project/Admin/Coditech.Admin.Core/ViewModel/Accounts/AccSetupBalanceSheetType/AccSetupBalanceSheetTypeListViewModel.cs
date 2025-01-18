using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public partial class AccSetupBalanceSheetTypeListViewModel : BaseViewModel
    {
        public List<AccSetupBalanceSheetTypeViewModel> AccSetupBalanceSheetTypeList { get; set; }
        public AccSetupBalanceSheetTypeListViewModel()
        {
            AccSetupBalanceSheetTypeList = new List<AccSetupBalanceSheetTypeViewModel>();
        }
    }
}
