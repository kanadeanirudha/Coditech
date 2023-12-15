using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralSystemGlobleSettingModel : BaseModel
    {
        [Required]
        public short GeneralSystemGlobleSettingMasterId { get; set; }

        [MaxLength(50)]
        [Required]
        public string FeatureName { get; set; }

        [MaxLength(200)]
        [Required]
        public string FeatureDefaultValue { get; set; }

        [MaxLength(200)]
        public string FeatureValue { get; set; }
    }
}
