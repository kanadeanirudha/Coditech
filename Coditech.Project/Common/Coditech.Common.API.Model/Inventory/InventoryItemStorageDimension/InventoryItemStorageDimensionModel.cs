namespace Coditech.Common.API.Model
{
    public class InventoryItemStorageDimensionModel : BaseModel
    {
        public InventoryItemStorageDimensionModel()
        {

        }
        public short InventoryItemStorageDimensionId { get; set; }
        public string StorageDimensionName { get; set; }
        public string StorageDimensionCode { get; set; }      
    
    }
}
