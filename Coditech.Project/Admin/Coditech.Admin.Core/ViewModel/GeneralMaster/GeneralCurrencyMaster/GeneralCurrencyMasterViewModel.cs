using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralCurrencyMasterViewModel : BaseViewModel
    {
        public short GeneralCurrencyMasterId { get; set; }
        [Required]
        [Display(Name = "Currency Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string CurrencyName { get; set; }

        [Display(Name = "Currency Code")]
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string CurrencyCode { get; set; }
        [Display(Name = "Currency Symbol")]
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string CurrencySymbol { get; set; }
    }
}
