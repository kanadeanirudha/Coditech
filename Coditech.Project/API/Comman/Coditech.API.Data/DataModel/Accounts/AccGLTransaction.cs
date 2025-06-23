using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Coditech.API.Data
{
    public partial class AccGLTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AccGLTransactionId { get; set; }
        public int AccSetupBalanceSheetId{ get; set; }
        public byte GeneralFinancialYearId { get; set; }
        public byte AccSetupTransactionTypeId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string NarrationDescription { get; set; }
        public string VoucherNumber { get; set; }
        public bool IsPosted { get; set; }
        public long PostedBy { get; set; }
        public DateTime  PostedDate { get; set; }
        public int TransactionEnum { get; set; }
        public string TransactionRefId { get; set; }
        public short PaymentModeEnum { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
