using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralMessagesViewModel : BaseViewModel
    {
        public byte OTPLength { get; set; }
        public bool IsSendOnMobile { get; set; }
        public bool IsSendOnEmail { get; set; }
        public bool IsSendOnWhatsapp { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string CentreCode { get; set; }
        public string CallingCode { get; set; }
        public string OTP { get; set; }
    }
}
