using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Coditech.API.Data
{
    public partial class AccSetupBalanceSheetType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte AccSetupBalanceSheetTypeId { get; set; }
        public string AccBalsheetTypeCode { get; set; }
        public string AccBalsheetTypeDesc { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemGenerated { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
