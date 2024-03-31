using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class InventoryCategory
    {
        [Key]
        public short InventoryCategoryId { get; set; }
        public short ParentInventoryCategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string ItemPrefix { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}

