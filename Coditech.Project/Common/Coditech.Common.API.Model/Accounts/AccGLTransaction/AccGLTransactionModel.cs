using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public partial class AccGLTransactionModel : BaseModel
    {
        public long AccGLTransactionId { get; set; }
        public int AccSetupBalanceSheetId { get; set; }
        public short GeneralFinancialYearId{ get; set; }
        public short AccSetupTransactionTypeId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string NarrationDescription  { get; set; }
        public string VoucherNumber { get; set; }
        public int TransactionEnumId { get; set; }
        public string TransactionRefNumber { get; set; }
        public string ModeCode { get; set; }
        public bool IsActive { get; set; }
     

    }
}

