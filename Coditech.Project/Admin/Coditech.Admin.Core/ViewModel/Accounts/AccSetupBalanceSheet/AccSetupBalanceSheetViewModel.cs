using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public partial class AccSetupBalanceSheetViewModel : BaseViewModel
    {
        public int AccSetupBalanceSheetId { get; set; }
        [Display(Name = "BalanceSheet Type")]
        public byte AccSetupBalanceSheetTypeId { get; set; }

        [Display(Name = "Account Balancesheet Name")]
        [MaxLength(50)]
        public string AccBalancesheetHeadDesc { get; set; }
        [MaxLength(20)]
        public string AccBalancesheetCode { get; set; }
        [Display(Name = "Centre Code")]
        [MaxLength(15)]
        public string CentreCode { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Is System Generated")]
        public bool IsSystemGenerated { get; set; }
        public string AccBalsheetTypeDesc { get; set; }

    }
}
