namespace Coditech.API.Data
{
    public partial class OrganisationCentrewiseSmtpSetting
    {
        public short OrganisationCentrewiseSmtpSettingId { get; set; }
        public string CentreCode { get; set; }
        public string ServerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool IsEnableSsl { get; set; }
        public string FromDisplayName { get; set; }
        public string FromEmailAddress { get; set; }
        public string BccEmailAddress { get; set; }
        public bool DisableAllEmails { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
