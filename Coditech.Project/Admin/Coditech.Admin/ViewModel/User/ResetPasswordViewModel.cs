using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class ResetPasswordViewModel : BaseViewModel
    {
        [MaxLength(100)]
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
    }
}