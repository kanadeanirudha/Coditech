namespace Coditech.API.Data
{
    public partial class GeneralCountryMaster : BaseDataModel
    {
        public short GeneralCountryMasterId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public bool IsUserDefined { get; set; }
        public Nullable<bool> DefaultFlag { get; set; }
        public Nullable<int> SeqNo { get; set; }
        public string CurrencyCode { get; set; }
        public string currencySymbol { get; set; }
        public byte[] CountryFlag { get; set; }
    }
}

