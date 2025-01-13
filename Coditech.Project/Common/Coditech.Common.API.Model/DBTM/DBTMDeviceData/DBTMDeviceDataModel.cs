using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class DBTMDeviceDataModel : BaseModel
    {
        public long DBTMDeviceDataId { get; set; }
        [MaxLength(100)]
        [Required]
        public string DeviceSerialCode { get; set; }
        [MaxLength(200)]
        [Required]
        public string PersonCode { get; set; }
        [MaxLength(50)]
        [Required]
        public string TestCode { get; set; }
        [MaxLength(200)]
        public string Comments { get; set; }
        [Required(ErrorMessage = "Please enter weight.")]
        public decimal Weight { get; set; }
        [Required]
        public decimal Height { get; set; }
        [Required]
        public long Time { get; set; }
        [Required]
        public decimal Distance { get; set; }
        [Required]
        public decimal Force { get; set; }
        [Required]
        public decimal Acceleration { get; set; }
        [Required]
        public decimal Angle { get; set; }
    }
}
