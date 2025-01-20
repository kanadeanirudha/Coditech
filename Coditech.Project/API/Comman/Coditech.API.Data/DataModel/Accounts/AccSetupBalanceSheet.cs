using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Coditech.API.Data
{
    public partial class AccSetupBalanceSheet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccSetupBalanceSheetId { get; set; }
        public byte AccSetupBalanceSheetTypeId { get; set; }
        public string AccBalancesheetCode { get; set; }
        public string AccBalancesheetHeadDesc { get; set; }
        public string CentreCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemGenerated { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
