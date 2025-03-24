using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class InventoryCategoryTypeMaster
    {
        [Key]
        public byte InventoryCategoryTypeMasterId { get; set; }
        public string CategoryTypeName { get; set; }
        public bool IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

