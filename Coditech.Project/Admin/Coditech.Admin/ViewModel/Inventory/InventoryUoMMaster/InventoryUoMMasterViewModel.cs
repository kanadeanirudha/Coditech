using Coditech.Common.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class InventoryUoMMasterViewModel : BaseViewModel
    {
        public Int16 InventoryUoMMasterId { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Uom Code")]
        public string UomCode { get; set; }
        [Required]
        [MaxLength(200)]
        [DisplayName("Uom Description")]
        public string UomDescription { get; set; }
        [MaxLength(200)]
        [DisplayName("Commercial Description")]
        public string CommercialDescription { get; set; }
        [Required]
        [DisplayName("Measurement Unit")]
        public short GeneralMeasurementUnitMasterId { get; set; }
        public string MeasurementUnitDisplayName { get; set; }

        [DisplayName("Convertion Factor")]
        public decimal ConvertionFactor { get; set; }

        [DisplayName("Additive Constant")]
        [MaxLength(10)]
        public string AdditiveConstant { get; set; }

        [Required]
        [DisplayName("Decimal Places Upto")]
        public Int16 DecimalPlacesUpto { get; set; }

        [DisplayName("Decimal Rounding")]
        public Int16 DecimalRounding { get; set; }
    }
}
