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
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

