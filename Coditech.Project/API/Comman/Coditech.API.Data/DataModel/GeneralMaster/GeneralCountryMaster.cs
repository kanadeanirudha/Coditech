namespace Coditech.API.Data
{
    public partial class GeneralCountryMaster
    {
        public short GeneralCountryMasterId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CallingCode { get; set; }
        public bool IsUserDefined { get; set; }
        public bool DefaultFlag { get; set; }
        public Nullable<int> SeqNo { get; set; }
        public string CurrencyCode { get; set; }
        public string currencySymbol { get; set; }
        public byte[] CountryFlag { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

