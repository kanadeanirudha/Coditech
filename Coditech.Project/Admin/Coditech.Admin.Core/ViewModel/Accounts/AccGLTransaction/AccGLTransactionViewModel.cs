using Coditech.Common.Helper;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public class AccGLTransactionViewModel : BaseViewModel
    {
        public long AccGLTransactionId { get; set; }
        [Display(Name = "Balance Sheet ")]
        public int AccSetupBalanceSheetId { get; set; }
        public short GeneralFinancialYearId { get; set; }
        public short AccSetupTransactionTypeId { get; set; }
        public byte AccSetupBalanceSheetTypeId { get; set; }
        [Display(Name = "Centre Code")]
        [MaxLength(15)]
        public string CentreCode { get; set; }
        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        [Display(Name = "Narration Description")]
        [MaxLength(500)]
        public string NarrationDescription { get; set; }
        [MaxLength(100)]
        [Display(Name = "Voucher Number")]
        public string VoucherNumber { get; set; }
        [Display(Name = "Transaction  Number")]
        public int TransactionEnum { get; set; }
        [Display(Name = "Transaction Refrence Number")]
        [MaxLength(200)]
        public string TransactionRefId { get; set; }
        [MaxLength(30)]
        [Display(Name = "Mode Code")]
        public string ModeCode { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
