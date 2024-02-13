using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coditech.Admin.ViewModel
{
    public class GymMemberBodyMeasurementViewModel : BaseViewModel
    {
        [Required]
        [Display(Name = "Gym Member Body Measurement Id")]
        public long GymMemberBodyMeasurementId { get; set; }

        [ForeignKey("GymMemberDetailId")]
        [Required]
        [Display(Name = "Gym Member Detail Id")]
        public int GymMemberDetailId { get; set; }

        [Key]
        [Required]
        [Display(Name = "Gym Body Measurement Type Id")]
        public short GymBodyMeasurementTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Body Measurement Value")]
        public string BodyMeasurementValue { get; set; }
    }
}
