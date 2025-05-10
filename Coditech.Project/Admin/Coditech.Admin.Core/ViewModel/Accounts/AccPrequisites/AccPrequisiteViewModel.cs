using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public class AccPrequisiteViewModel : BaseViewModel
    {
        public int BalanceSheetId { get; set; }
        [Display(Name = "Currency ")]
        public bool IsCurrencyAssociated { get; set; }

        [Display(Name = "Financial Year")]
        public bool IsFinacialYearAssociated { get; set; }

        [Display(Name = "Balance Sheet")]
        public bool IsBalanceSheetAssociated { get; set; }
        [Display(Name = "GL Accounts ")]
        public bool IsAccGLBalanceSheetAssociated { get; set; }
    }
}
