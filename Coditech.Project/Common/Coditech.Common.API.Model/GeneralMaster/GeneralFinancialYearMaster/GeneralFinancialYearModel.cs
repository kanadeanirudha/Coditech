namespace Coditech.Common.API.Model
{
    public class GeneralFinancialYearModel : BaseModel
    {
       
        public short GeneralFinancialYearId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
