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
        public string MeasurementUnitDisplayName { get; set; }
        [Display(Name = "Unit Short Code")]
        [Required]
        [MaxLength(50)] 
        public string MeasurementUnitShortCode { get; set; }
    }
}
