namespace Coditech.Common.API.Model
{
    public partial class AccPrequisiteModel : BaseModel
    {
        public bool IsBalanceSheetAssociated { get; set; }
        public bool IsCurrencyAssociated { get; set; }
        public bool IsFinacialYearAssociated { get; set; }
        public bool IsAccGLBalanceSheetAssociated { get; set; }
        public short GeneralCurrencyMasterId { get; set; }
        public string CurrencySymbol { get; set; }
        public string Name { get; set; }
        public string Field { get; set; }
        public bool IsAssociated { get; set; } = true;
    }
}
