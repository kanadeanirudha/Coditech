using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class UserLoginViewModel : BaseViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        //[Display(Name = ZnodeAdmin_Resources.RememberMe, ResourceType = typeof(Admin_Resources))]
        public bool RememberMe { get; set; }
    }
}