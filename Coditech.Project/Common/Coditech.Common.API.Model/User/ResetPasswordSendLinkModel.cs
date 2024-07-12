namespace Coditech.Common.API.Model
{
    public class ResetPasswordSendLinkModel : BaseModel
    {
        public string UserName { get; set; }
        public string ResetPasswordToken { get; set; }
    }
}
