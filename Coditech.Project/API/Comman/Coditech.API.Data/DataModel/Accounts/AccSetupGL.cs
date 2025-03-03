using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Coditech.API.Data
{
    public class AccSetupGL
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccSetupGLId { get; set; }
        public byte AccSetupCategoryId { get; set; }
        public int? AltSetupGLId { get; set; }
        public int? ParentAccSetupGLId { get; set; }
        public short? AccSetupGLTypeId { get; set; }
        public int AccSetupBalanceSheetId { get; set; }
        public byte? AccSetupChartOfAccountTemplateId { get; set; }
        public string GLName { get; set; }
        public string GLCode { get; set; }
        public bool IsGroup { get; set; }
        public Nullable <short> UserTypeEnum { get; set; }
        public byte DebitCreditEnum { get; set; }
        public bool IsOpBalRequired { get; set; }
        public int PrintingSequence { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemGenerated { get; set; }
        public  Nullable<byte> IsControlHeadEnum { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

    }
}
