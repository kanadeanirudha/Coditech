using Coditech.Common.API.Model;
using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class ACCGLOpeningBalanceListViewModel : BaseViewModel
    {
        public List<ACCGLOpeningBalanceViewModel> ACCGLOpeningBalanceList { get; set; }
        public ACCGLOpeningBalanceListViewModel()
        {
            ACCGLOpeningBalanceList = new List<ACCGLOpeningBalanceViewModel>();
        }
        public int AccGLOpeningBalanceId { get; set; }
        public short AccSetupCategoryId { get; set; }
        public int AccSetupGLId { get; set; }
        public string AccGLOpeningBalanceData { get; set; }
        public List<ACCGLOpeningBalanceModel> ACCGLBalanceList { get; set; }
        public GeneralFinancialYearModel GeneralFinancialYearModel { get; set; }
    }
}
