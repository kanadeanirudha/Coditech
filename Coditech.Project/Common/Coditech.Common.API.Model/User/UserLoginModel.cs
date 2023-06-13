using System.ComponentModel.DataAnnotations;
namespace Coditech.Common.API.Model
{
    public class UserLoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}