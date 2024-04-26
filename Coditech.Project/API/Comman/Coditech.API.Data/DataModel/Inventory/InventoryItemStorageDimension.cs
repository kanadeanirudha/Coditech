using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class InventoryItemStorageDimension
    {
        [Key]
        public short InventoryItemStorageDimensionId { get; set; }
         public string StorageDimensionName { get; set; }
        public string StorageDimensionCode { get; set; }
        public short? ParentInventoryItemStorageDimensionId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}

