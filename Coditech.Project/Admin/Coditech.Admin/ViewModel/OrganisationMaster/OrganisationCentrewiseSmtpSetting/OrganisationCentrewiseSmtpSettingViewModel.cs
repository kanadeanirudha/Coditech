using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseSmtpSettingViewModel : BaseViewModel
    {
        [Display(Name = "Centre Name")]
        public string CentreName { get; set; }

        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string ServerName { get; set; }
    }
}
