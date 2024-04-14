using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class ChangePasswordModel : BaseModel
    {
        public long UserMasterId { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required/*(ErrorMessage = "Please Entre The New Password")*/]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
