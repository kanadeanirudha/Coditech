namespace Coditech.Common.API.Model
{
    public partial class OrganisationCentrewiseSmtpSettingSendTestEmailModel : BaseModel
    {
        public string TO { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CentreCode { get; set; }
        public bool IsWhatsappMessage { get; set; }
        public bool IsSmsMessage { get; set; }
        public bool IsEmailMessage { get; set; }
        public string MobileNumber { get; set; }
        public string Message { get; set; }
    }
}
