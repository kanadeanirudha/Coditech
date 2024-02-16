using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralFinancialYearViewModel : BaseViewModel
    {
        public short GeneralFinancialYearMasterId { get; set; }
        [Required]
        [Display(Name = "FromDate")]
        public DateTime FromDate { get; set; }
        [Required]

        [Display(Name = "ToDate")]
        public DateTime ToDate { get; set; }
       
      }
}
