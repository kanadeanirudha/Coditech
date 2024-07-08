using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class ResetPasswordModel : BaseModel
    {
        public string UserName { get; set; }
        public string ResetPasswordToken { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
