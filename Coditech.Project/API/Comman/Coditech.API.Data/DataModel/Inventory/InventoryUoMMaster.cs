using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coditech.API.Data
{
    public class InventoryUoMMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int16 InventoryUoMMasterId { get; set; }
        public string UomCode { get; set; }
        public string UomDescription { get; set; }
        public string CommercialDescription { get; set; }
        public short GeneralMeasurementUnitMasterId { get; set; }
        [NotMapped]
        public string MeasurementUnitDisplayName { get; set; }
        public decimal ConvertionFactor { get; set; }
        public string AdditiveConstant { get; set; }
        public byte DecimalPlacesUpto { get; set; }
        public byte DecimalRounding { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
