using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class ResetPasswordSendLinkModel : BaseModel
    {
        public string UserName { get; set; }
        public object ResetPasswordToken { get; set; }

        // public string ResetPasswordUrl { get; set; }
    }
}
