using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Coditech.API.Data
{
    public class AccSetupGLBank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccSetupGLBankId { get; set; }
        public int AccSetupBalanceSheetId { get; set; }
        public int AccSetupGLId { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankBranchName { get; set; }
        public decimal BankLimitAmount { get; set; }
        public decimal RateOfInterest { get; set; }
        public string InterestMode { get; set; }
        public DateTime OpenDatetime { get; set; }
        public DateTime DueDatetime { get; set; }
        public int InterestTypeEnumId { get; set; }
        public string IFSCCode { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
