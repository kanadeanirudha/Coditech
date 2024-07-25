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

        public string ResetPasswordToken { get; set; }

        [MaxLength(10)]
        [Required]
        [Display(Name = "OTP")]
        public string OTP { get; set; }

        [MaxLength(100)]
        [MinLength(8)]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [MaxLength(100)]
        [MinLength(8)]
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}