namespace Coditech.Common.Service
{
    public interface ICoditechWhatsApp
    {
        void SendWhatsAppMessage(string centreCode, string smsText, string phoneNumber);
    }
}