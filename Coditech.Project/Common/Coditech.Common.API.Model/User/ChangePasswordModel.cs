using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class ChangePasswordModel : BaseModel
    {
        public long EntityId { get; set; }
        public string UserType { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required/*(ErrorMessage = "Please Entre The New Password")*/]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
    }
}
