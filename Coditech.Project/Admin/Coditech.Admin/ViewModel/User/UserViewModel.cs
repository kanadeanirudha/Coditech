using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsAdminUser { get; set; }
    }
}