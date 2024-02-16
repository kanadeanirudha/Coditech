using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralFinancialYearMaster
    {
        [Key]
        public short GeneralFinancialYearMasterId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        }
    }


