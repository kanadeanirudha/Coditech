using System;
using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class InventoryStorageDimensionGroup
    {
        [Key]
        public int InventoryStorageDimensionGroupId { get; set; }

        public string StorageDimensionGroupName { get; set; }
        public string StorageDimensionGroupCode { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool WarehouseManagementProcesses { get; set; }
        public bool Mandatory { get; set; }
        public bool PrimaryStocking { get; set; }
    }
}
