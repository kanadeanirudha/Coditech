using Coditech.Common.API.Model;
using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class AccGLIndividualOpeningBalanceViewModel : BaseViewModel
    {
        public AccGLIndividualOpeningBalanceViewModel()
        {
            AccGLIndividualOpeningBalanceList = new List<AccGLIndividualOpeningBalanceModel>();
        }
        public List<AccGLIndividualOpeningBalanceModel> AccGLIndividualOpeningBalanceList { get; set; }
        public int AccGLIndividualOpeningBalanceId { get; set; }
        public short GeneralFinancialYearId { get; set; }
        public int AccSetupGLId { get; set; }
        public int AccSetupBalanceSheetId { get; set; }
        public string UserType { get; set; }
        public long PersonId { get; set; }
        public DateTime OpeningDatetime { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal TotalDebitAmount { get; set; }
        public decimal TotalCreditAmount { get; set; }
        public decimal ClosingBalance { get; set; }
        public bool IsActive { get; set; }
        public string PersonName { get; set; }
        public string LastName { get; set; }
        public byte DebitCreditEnum { get; set; }
        public short UserTypeId { get; set; }
        public string IndividualOpeningBalanceData { get; set; }
    }
}
