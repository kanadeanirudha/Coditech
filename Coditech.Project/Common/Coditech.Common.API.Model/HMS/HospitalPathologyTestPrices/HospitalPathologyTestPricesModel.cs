using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class HospitalPathologyTestPricesModel : BaseModel
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
        public string PathologyTestName { get; set; }
        public string HospitalPathologyPriceCategory { get; set; }
    }
}
