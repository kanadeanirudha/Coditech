using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class ResetPasswordSendLinkViewModel : BaseViewModel
    {
        [MaxLength(100)]
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        public string ResetPasswordToken { get; set; }
    }
}