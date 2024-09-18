using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class HospitalPathologyTestPrices
    {
        [Key]
        public long HospitalPathologyTestPricesId { get; set; }
        public int HospitalPathologyPriceCategoryEnumId { get; set; }
        public int HospitalPathologyTestId { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

