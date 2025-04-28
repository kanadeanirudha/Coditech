using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public partial class AccGLTransactionDetailsModel : BaseModel
    {
        public long AccGLTransactionDetailsId { get; set; }
        public long AccGLTransactionId { get; set; }
        public int AccSetupGLId { get; set; }
        public short AccSetupTransactionTypeId { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public int RefAccSetupGLId { get; set; }
        public byte DebitCreditEnum { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDatetime { get; set; }
        public string NarrationDescription { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public DateTime BankClearingDatetime { get; set; }
        public string UserType { get; set; }
        public DateTime SubmitDatetime { get; set; }
        public string SubmitSlipNo { get; set; }
        public DateTime ReconcilationDatetime { get; set; }
        public string ModeCode { get; set; }
        public DateTime OpeningDatetime { get; set; }
        public bool IsActive { get; set; }
        public string DetailsTransactionData { get; set; }
        public string AccGLName { get; set; }
    }
}

