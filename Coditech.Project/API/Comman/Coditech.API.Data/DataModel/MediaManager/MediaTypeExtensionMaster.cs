using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class MediaTypeExtensionMaster
    {
        [Key]
        public short MediaTypeExtensionMasterId { get; set; }
        public byte MediaTypeMasterId { get; set; }
        public string ExtensionName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
