using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPathologyTestPricesViewModel : BaseViewModel
    {
        public long HospitalPathologyTestPricesId { get; set; }
        [Required]
        public int HospitalPathologyPriceCategoryEnumId { get; set; }
        [Required]
        public int HospitalPathologyTestId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Display(Name = "Pathology Test Name")]
        public string PathologyTestName { get; set; }
        [Display(Name = "Hospital Pathology Price Category")]
        public string HospitalPathologyPriceCategory { get; set; }
    }
}