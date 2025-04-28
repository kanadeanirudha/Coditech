namespace Coditech.Common.API.Model
{
    public partial class ACCGLOpeningBalanceModel : BaseModel
    {
        public int AccGLOpeningBalanceId { get; set; }
        public short GeneralFinancialYearId { get; set; }
        public int AccSetupGLId { get; set; }
        public int AccSetupBalanceSheetId { get; set; }
        public DateTime OpeningDatetime { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal TotalDebitAmount { get; set; }
        public decimal TotalCreditAmount { get; set; }
        public decimal ClosingBalance { get; set; }
        public bool IsActive { get; set; }
        public string CategoryName { get; set; }
        public string GLName { get; set; }
        public byte DebitCreditEnum { get; set; }
        public short AccSetupCategoryId { get; set; }
        public int UserTypeId { get; set; }
        public List<ACCGLOpeningBalanceModel> ACCGLBalanceList { get; set; }
        public List<ACCGLOpeningBalanceModel> ACCGLOpeningBalanceList { get; set; }
    }
}

