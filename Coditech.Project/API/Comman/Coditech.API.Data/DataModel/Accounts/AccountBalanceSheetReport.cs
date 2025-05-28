using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Coditech.API.Data
{
    public partial class AccountBalanceSheetReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccSetupBalanceSheetId { get; set; }
        public short GeneralFinancialYearId { get; set; }
        public int AccSetupGLId { get; set; }
        public string GLName { get; set; }
        public bool IsGroup { get; set; }
        public string CategoryName { get; set; }
        public decimal ClosingBalance { get; set; }
        public string SelectedCentreCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}


