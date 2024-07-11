using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class ChangePasswordViewModel : BaseViewModel

    {
        public long UserMasterId { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [MaxLength(100)]
        [MinLength(8)]
        [Required/*(ErrorMessage = "Please Entre The New Password")*/]
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
