﻿using System.ComponentModel.DataAnnotations;
using Coditech.Common.API.Model;
using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class AccGLTransactionViewModel : BaseViewModel
    {
        public long AccGLTransactionId { get; set; }
        [Required]
        [Display(Name = "Balance Sheet ")]
        public int AccSetupBalanceSheetId { get; set; }
        [Required]
        [Display(Name = "Financial Year ")]
        public short GeneralFinancialYearId { get; set; }
        [Required]
        [Display(Name = "Transaction Type ")]
        public short AccSetupTransactionTypeId { get; set; }
        [Required]
        [Display(Name = "Balancesheet Type ")]
        public byte AccSetupBalanceSheetTypeId { get; set; }
        [Display(Name = "Centre Code")]
        [MaxLength(15)]
        public string CentreCode { get; set; }
        [Required]
        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        [Required]
        [Display(Name = "Narration Description")]
        [MaxLength(500)]
        public string NarrationDescription { get; set; }
        [MaxLength(100)]
        [Display(Name = "Voucher Number")]
        public string VoucherNumber { get; set; }
        [Display(Name = "Transaction  Number")]
        public int TransactionEnum { get; set; }
        [Required]
        [Display(Name = "Transaction Reference Number")]
        [MaxLength(200)]
        public string TransactionRefId { get; set; }
        [MaxLength(30)]
        [Display(Name = "Mode Code")]
        public string ModeCode { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Required]
        [Display(Name = "Centre")]
        public string SelectedCentreCode { get; set; }
        public string SearchKeywords { get; set; }
        public string TransactionDetailsData { get; set; }
        public List<AccGLTransactionDetailsModel> AccGLTransactionDetailsList { get; set; }
        public List<AccSetupGLModel> AccSetupGLList { get; set; }
        public List<AccSetupGLModel> SuggestionsAccSetupGLList { get; set; }
        public List<AccGLIndividualOpeningBalanceModel> Personlist { get; set; }


    }
}
