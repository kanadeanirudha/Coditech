namespace Coditech.Common.API.Model
{
    public class GeneralFinancialYearModel : BaseModel
    {
        public short GeneralFinancialYearId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CentreCode { get; set; }
        public bool IsYearEnd { get; set; }
        public bool IsCurrentFinancialYear { get; set; }
    }
}
