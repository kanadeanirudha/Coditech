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

        public string CentreCode { get; set; }
        public string SelectedCentreCode { get; set; }

    }
}
