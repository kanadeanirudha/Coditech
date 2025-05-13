using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralFinancialYearViewModel : BaseViewModel
    {
        public short GeneralFinancialYearId { get; set; }
        [Required]
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; } = DateTime.Now;
        [Required]

        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; } = DateTime.Now;
        [Display(Name = "Centre")]
        public string CentreCode { get; set; }
        [Display(Name = "Is Year End")]
        public bool IsYearEnd { get; set; }
        [Display(Name = "Is Current Financial Year")]
        public bool IsCurrentFinancialYear { get; set; }

    }
}
