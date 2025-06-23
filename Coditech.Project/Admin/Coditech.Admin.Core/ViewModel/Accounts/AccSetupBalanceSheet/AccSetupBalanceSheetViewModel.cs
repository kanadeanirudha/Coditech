using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public partial class AccSetupBalanceSheetViewModel : BaseViewModel
    {
        public int AccSetupBalanceSheetId { get; set; }
        [Display(Name = "Balance Sheet Type")]
        public byte AccSetupBalanceSheetTypeId { get; set; }

        [Display(Name = "Balance Sheet Name")]
        [MaxLength(50)]
        public string AccBalancesheetHeadDesc { get; set; }
        [MaxLength(20)]
        [Display(Name = "Balance Sheet Code")]
        public string AccBalancesheetCode { get; set; }
        [Required]
        [Display(Name = "Centre")]
        [MaxLength(15)]
        public string CentreCode { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Is System Generated")]
        public bool IsSystemGenerated { get; set; }
        public string AccBalsheetTypeDesc { get; set; }
    }
}
