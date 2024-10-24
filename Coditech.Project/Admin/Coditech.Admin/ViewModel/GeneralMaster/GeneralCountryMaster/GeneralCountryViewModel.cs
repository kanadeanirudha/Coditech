using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralCountryViewModel : BaseViewModel
    {
        public short GeneralCountryMasterId { get; set; }
        [Required]
        [Display(Name = "Country Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string CountryName { get; set; }

        [Display(Name = "Country Code")]
        [Required]
        public string CountryCode { get; set; }
        [Display(Name = "Is Default")]
        public bool DefaultFlag { get; set; }

        [Display(Name = "Seq Number")]
        [Required]
        public short SeqNo { get; set; }

        [Display(Name = "Calling Code")]
        [Required]
        public string CallingCode { get; set; }
    }
}
