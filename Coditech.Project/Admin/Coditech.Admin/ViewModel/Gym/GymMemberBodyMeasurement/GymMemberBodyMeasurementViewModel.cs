using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GymMemberBodyMeasurementViewModel : BaseViewModel
    {
        [Required]
        public long GymMemberBodyMeasurementId { get; set; }

        [Required]
        public int GymMemberDetailId { get; set; }

        [Required]
        public short GymBodyMeasurementTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Body Measurement Value")]
        public string BodyMeasurementValue { get; set; }

        public string BodyMeasurementType { get; set; }
        public string MeasurementUnitShortCode { get; set; }
        public string MeasurementUnitDisplayName { get; set; }
    }
}
