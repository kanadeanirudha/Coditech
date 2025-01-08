using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class CoditechApplicationSettingViewModel : BaseViewModel
    {
        [Required]
        public short CoditechApplicationSettingId { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "Application Code")]
        public string ApplicationCode { get; set; }

        [MaxLength(500)]
        [Required]
        [Display(Name = "Application Value 1")]
        public string ApplicationValue1 { get; set; }

        [MaxLength(500)]
        [Display(Name = "Application Value 2")]
        public string ApplicationValue2 { get; set; }

        [MaxLength(500)]
        [Display(Name = "Application Value 3")]
        public string ApplicationValue3 { get; set; }
    }
}
