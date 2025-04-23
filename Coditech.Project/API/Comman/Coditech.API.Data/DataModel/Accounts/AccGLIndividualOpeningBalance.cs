using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Coditech.API.Data
{
    public partial class AccGLIndividualOpeningBalance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccGLIndividualOpeningBalanceId { get; set; }
        public short GeneralFinancialYearId { get; set; }
        public int AccSetupGLId { get; set; }
        public int AccSetupBalanceSheetId { get; set; }
        public long PersonId { get; set; }
        public DateTime OpeningDatetime { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal TotalDebitAmount { get; set; }
        public decimal TotalCreditAmount { get; set; }
        public decimal ClosingBalance { get; set; }
        public bool IsActive { get; set; }
        public short UserTypeId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
