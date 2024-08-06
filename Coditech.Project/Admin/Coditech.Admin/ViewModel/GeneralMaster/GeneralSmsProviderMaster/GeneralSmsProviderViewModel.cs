using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralSmsProviderViewModel : BaseViewModel
    {
        public short GeneralSmsProviderId { get; set; }

        [Required]
        [Display(Name = "Provider Name")]
        public string ProviderName { get; set; }
        [Required]
        [Display(Name = " Provider Code")]
        public string ProviderCode { get; set; }
        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
