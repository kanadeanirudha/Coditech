using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public partial class OrganisationCentrewiseSmtpSettingSendTestEmailViewModel : BaseViewModel
    {
        [Required]
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "To")]
        public string TO { get; set; }
        [Display(Name = "Cc")]
        public string CC { get; set; }
        [Display(Name = "Bcc")]
        public string BCC { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Body")]
        public string Body { get; set; }
        public string CentreCode { get; set; }
    }
}
