namespace Coditech.Common.API.Model
{
    public partial class AccSetupGLBankModel : BaseModel
    {
        public int AccSetupGLBankId { get; set; }
        public int AccSetupBalanceSheetId { get; set; }
        public int AccSetupGLId { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankBranchName { get; set; }
        public decimal BankLimitAmount { get; set; }
        public decimal RateOfInterest { get; set; }
        public string InterestMode { get; set; }
        public DateTime OpenDatetime { get; set; }
        public DateTime DueDatetime { get; set; }
        public int InterestTypeEnumId { get; set; }
        public string IFSCCode { get; set; }
        public bool IsActive { get; set; }
        string SelectedCentreCode { get; set; }
        byte AccSetupBalanceSheetTypeId { get; set; }

    }
}

