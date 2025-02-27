using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public partial class AccSetupGLViewModel : BaseViewModel
    {
        public int AccSetupGLId { get; set; }
        public string GLName { get; set; }
        public int ParentAccSetupGLId { get; set; }
        public decimal OpeningBalance { get; set; }
        public bool IsGroup { get; set; }
        public string CategoryCode { get; set; }
        public string SelectedCentreCode { get; set; }
        public int AccSetupBalanceSheetId { get; set; }
        public byte AccSetupBalanceSheetTypeId { get; set; }
        public List<AccSetupGLViewModel> SubAccounts { get; set; } = new List<AccSetupGLViewModel>();
    }
}
