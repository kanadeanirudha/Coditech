using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralOccupationViewModel : BaseViewModel
    {
        public short GeneralOccupationMasterId { get; set; }

        [Required]
        [MaxLength(60)]
        [Display(Name = "Occupation Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string OccupationName { get; set; }

        [Display(Name = "Display Order")]
        [Required]
        public short DisplayOrder { get; set; }
    }
}
