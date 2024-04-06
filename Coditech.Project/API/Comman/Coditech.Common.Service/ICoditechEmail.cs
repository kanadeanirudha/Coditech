namespace Coditech.Common.Service
{
    public interface ICoditechEmail
    {
        void SendEmail(string centreCode, string to, string from, string cc, string bcc, string subject, string body, bool isHtmlEmail, string attachedPath = "");
        void SendEmail(string centreCode, string to, string from, string cc, string bcc, string subject, string body);
        void SendEmail(string centreCode, string to, string from, string subject, string body);
    }
}