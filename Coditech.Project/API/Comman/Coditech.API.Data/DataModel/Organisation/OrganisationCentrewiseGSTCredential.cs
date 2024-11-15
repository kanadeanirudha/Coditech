using System;

namespace Coditech.API.Data
{
    public partial class OrganisationCentrewiseGSTCredential
    {
        public int OrganisationCentrewiseGSTCredentialId { get; set; }
        public int OrganisationCentreMasterId { get; set; }
        public string Version { get; set; }
        public string Urls { get; set; }
        public string EInvoiceUserName { get; set; }
        public string EInvoicePassword { get; set; }
        public string  AspId { get; set; }
        public string AspUserPassword { get; set; }
        public int QrCodeSize { get; set; }
        public string AuthToken { get; set; }
        public string TokenExpiry { get; set; }
        public string ClientId { get; set; }
        public bool IsLiveMode { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
