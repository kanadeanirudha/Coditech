using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralEnumaratorMaster
    {
        [Key]
        public int GeneralEnumaratorId { get; set; }
        public int GeneralEnumaratorGroupId { get; set; }
        public string EnumName { get; set; }
        public string EnumDisplayText { get; set; }
        public string RelatedWith { get; set; }
        public short EnumValue { get; set; }
        public short SequenceNumber { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}




