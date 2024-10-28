using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralMeasurementUnitViewModel : BaseViewModel
    {
        public short GeneralMeasurementUnitMasterId { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Unit Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string MeasurementUnitDisplayName { get; set; }
        [Display(Name = "Unit Short Code")]
        [Required]
        [MaxLength(50)] 
        public string MeasurementUnitShortCode { get; set; }
    }
}
