using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coditech.API.Data
{
    public partial class GymMemberBodyMeasurement
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
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

