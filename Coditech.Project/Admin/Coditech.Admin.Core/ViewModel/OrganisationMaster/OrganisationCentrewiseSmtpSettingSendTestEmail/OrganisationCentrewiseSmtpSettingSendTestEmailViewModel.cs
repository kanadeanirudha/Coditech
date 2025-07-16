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
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter valid mobile number")]
        [MaxLength(10)]
        [Required]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Message")]
        public string Message { get; set; }
        public bool IsSmsMessage { get; set; }
        public bool IsEmailMessage { get; set; }
        public bool IsWhatsappMessage { get; set; }
        public int OrganisationCentreMasterId { get; set; }
    }
}
