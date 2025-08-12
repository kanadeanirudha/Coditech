using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class ChangePasswordViewModel : BaseViewModel

    {
        public long EntityId { get; set; }
        public string UserType { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [MaxLength(100)]
        [MinLength(8)]
        [Required(ErrorMessage = "New Password Is Required")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&#])[A-Za-z\\d@$!%*?&#]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character (@$!%*?&#).")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [MaxLength(100)]
        [MinLength(8)]
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}
