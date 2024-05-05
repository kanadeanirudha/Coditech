using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class InventoryUoMMasterModel : BaseModel
    {
        [Required]
        public Int16 InventoryUoMMasterId { get; set; }
        [Required]
        [MaxLength(50)]
        public string UomCode { get; set; }
        [Required]
        [MaxLength(200)]
        public string UomDescription { get; set; }
        [MaxLength(200)]
        public string CommercialDescription { get; set; }
        [Required]
        public short GeneralMeasurementUnitMasterId { get; set; }
        public string MeasurementUnitDisplayName { get; set; }
        public decimal ConvertionFactor { get; set; }
        [MaxLength(10)]
        public string AdditiveConstant { get; set; }
        [Required]
        public Int16 DecimalPlacesUpto { get; set; }
        public Int16 DecimalRounding { get; set; }
    }
}
