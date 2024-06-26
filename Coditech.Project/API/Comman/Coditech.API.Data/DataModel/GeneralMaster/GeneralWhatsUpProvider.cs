namespace Coditech.API.Data
{
    public partial class GeneralWhatsUpProvider
    {
        public byte GeneralWhatsUpProviderId { get; set; }
        public string ProviderName { get; set; }
        public string ProviderCode { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

