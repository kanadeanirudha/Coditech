using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coditech.Common.API.Model
{
    public class GymMemberBodyMeasurementModel : BaseModel
    {
        [Required]
        public long GymMemberBodyMeasurementId { get; set; }

        [ForeignKey("GymMemberDetailId")]
        [Required]
        public int GymMemberDetailId { get; set; } 

        [Key]
        [Required]
        public short GymBodyMeasurementTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string BodyMeasurementValue { get; set; } 
    }
}
