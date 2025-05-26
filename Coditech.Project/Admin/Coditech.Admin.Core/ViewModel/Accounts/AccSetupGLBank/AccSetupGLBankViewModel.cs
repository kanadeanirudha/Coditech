using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{ 
    public partial class AccSetupGLBankViewModel : BaseViewModel
    {
        public int AccSetupGLBankId { get; set; }
        public int ParentAccSetupGLId { get; set; }
        public int AccSetupBalanceSheetId { get; set; }
        public int AccSetupGLId { get; set; }
        [MaxLength(20)]
        [Display(Name = "Account Number")]
        public string BankAccountNumber { get; set; }
        [Display(Name = "Account Name")]
        public string BankAccountName { get; set; }
        [Display(Name = "Branch Name")]
        public string BankBranchName { get; set; }
        [Display(Name = " Bank Limit Amount")]
        public decimal BankLimitAmount { get; set; }
        public decimal RateOfInterest { get; set; }
        public string InterestMode { get; set; }
        public DateTime OpenDatetime { get; set; } = DateTime.Now;
        public DateTime DueDatetime { get; set; } = DateTime.Now;
        public int InterestTypeEnumId { get; set; }
        [Display(Name = "IFSC Code")]
        [MaxLength(11)]
        public string IFSCCode { get; set; }
        public short AccSetupCategoryId { get; set; }
        public bool IsActive { get; set; }
        public string BankModelData { get; set; }

    }
}
