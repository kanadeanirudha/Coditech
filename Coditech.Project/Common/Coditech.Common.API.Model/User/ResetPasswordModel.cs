namespace Coditech.Common.API.Model
{
    public class ResetPasswordModel : BaseModel
    {
        public string ResetPasswordToken { get; set; }
        public string NewPassword { get; set; }
        public string OTP { get; set; }
    }
}
