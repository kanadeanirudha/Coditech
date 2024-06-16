namespace Coditech.Common.Service
{
    public interface ICoditechSMS
    {
        void SendSMS(string centreCode, string smsText, string phoneNumber);
    }
}