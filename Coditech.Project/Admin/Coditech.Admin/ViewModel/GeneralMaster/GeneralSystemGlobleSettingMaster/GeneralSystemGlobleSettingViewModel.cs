using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralSystemGlobleSettingViewModel : BaseViewModel
    {
        [Required]
        public short GeneralSystemGlobleSettingMasterId { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Feature Name")]
        public string FeatureName { get; set; }

        [MaxLength(200)]
        [Required]
        [Display(Name = "Feature Default Value")]
        public string FeatureDefaultValue { get; set; }

        [MaxLength(200)]
        [Display(Name = "Feature Value")]
        public string FeatureValue { get; set; }
    }
}
