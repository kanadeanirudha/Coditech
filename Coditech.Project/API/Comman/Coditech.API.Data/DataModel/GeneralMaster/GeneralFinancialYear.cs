using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralFinancialYear
    {
        [Key]
        public short GeneralFinancialYearId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        }
    }


