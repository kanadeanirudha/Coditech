using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class UserLoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}