using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralFinancialYear
    {
        [Key]
        public short GeneralFinancialYearId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsYearEnd { get; set; }
        public string CentreCode { get; set; }
        public bool IsCurrentFinancialYear { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
 }


