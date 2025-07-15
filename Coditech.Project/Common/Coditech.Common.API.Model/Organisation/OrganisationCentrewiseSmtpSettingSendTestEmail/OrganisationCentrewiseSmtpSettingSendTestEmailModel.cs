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
    }
}
