using Coditech.Common.API.Model;
using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class ACCGLOpeningBalanceViewModel : BaseViewModel
    {

        public ACCGLOpeningBalanceViewModel()
        {
            ACCGLOpeningBalanceList = new List<ACCGLOpeningBalanceModel>();
        }
        public List<ACCGLOpeningBalanceModel> ACCGLOpeningBalanceList { get; set; }
        public int AccGLOpeningBalanceId { get; set; }
        public short GeneralFinancialYearId { get; set; }
        public int AccSetupGLId { get; set; }
        public int AccSetupBalanceSheetId { get; set; }
        public DateTime OpeningDatetime { get; set; }
        public decimal? OpeningBalance { get; set; }
        public decimal TotalDebitAmount { get; set; }
        public decimal TotalCreditAmount { get; set; }
        public decimal ClosingBalance { get; set; }
        public bool IsActive { get; set; }
        public string CategoryName { get; set; }
        public string GLName { get; set; }
        public byte DebitCreditEnum { get; set; }
        public short AccSetupCategoryId { get; set; }
        public string AccGLOpeningBalanceData { get; set; }
        public GeneralFinancialYearModel GeneralFinancialYearModel { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsClosingBalanceUpdated { get; set; }
    }
}
