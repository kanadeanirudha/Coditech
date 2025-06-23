using System.ComponentModel.DataAnnotations;
using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public class AccountBalanceSheetReportViewModel : BaseViewModel
    {
        [Display(Name = "Balance Sheet ")]
        public int AccSetupBalanceSheetId { get; set; }
        [Display(Name = "Financial Year ")]
        public short GeneralFinancialYearId { get; set; }
        public int AccSetupGLId { get; set; }
        [Display(Name = "GL Account Name")]
        public string GLName { get; set; }
        public bool IsGroup { get; set; }
        public string CategoryName { get; set; }
        public decimal ClosingBalance { get; set; }
        [Required]
        public string SelectedCentreCode { get; set; }
        [Required]
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }
        [Required]
        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; }
        [Required]
        public bool IsRecordFound { get; set; } = true;



    }
}
