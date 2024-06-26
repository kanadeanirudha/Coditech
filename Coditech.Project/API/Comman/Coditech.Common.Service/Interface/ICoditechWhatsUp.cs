namespace Coditech.Common.Service
{
    public interface ICoditechWhatsUp
    {
        void SendWhatsUpMessage(string centreCode, string smsText, string phoneNumber);
    }
}