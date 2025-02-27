namespace Coditech.Common.API.Model
{
    public class GeneralCurrencyMasterModel : BaseModel
    {
        public short GeneralCurrencyMasterId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        
    }
}
