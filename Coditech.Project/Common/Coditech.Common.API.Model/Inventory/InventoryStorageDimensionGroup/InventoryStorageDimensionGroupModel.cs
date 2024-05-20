using System;
using System.Collections.Generic;

namespace Coditech.Common.API.Model
{
    public class InventoryStorageDimensionGroupModel : BaseModel
    {
        public InventoryStorageDimensionGroupModel()
        {
            InventoryStorageDimensionGroupMapperList = new List<InventoryStorageDimensionGroupMapperModel>();
        }

        public List<InventoryStorageDimensionGroupMapperModel> InventoryStorageDimensionGroupMapperList { get; set; }
        public int InventoryStorageDimensionGroupId { get; set; }
        public string StorageDimensionGroupName { get; set; }
        public string StorageDimensionGroupCode { get; set; }
        public bool WarehouseManagementProcesses { get; set; }
        public bool Mandatory { get; set; }
        public bool PrimaryStocking { get; set; }
    }
}
