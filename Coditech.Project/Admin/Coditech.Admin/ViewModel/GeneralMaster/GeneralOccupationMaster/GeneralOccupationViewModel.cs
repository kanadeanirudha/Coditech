using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralOccupationViewModel : BaseViewModel
    {
        public short GeneralOccupationMasterId { get; set; }
        [Required]
        [Display(Name = "Occupation Name")]
        public string OccupationName { get; set; }
        [Display(Name = "Display Order")]
        [Required]
        public short DisplayOrder { get; set; }
    }
}
