namespace Coditech.Common.API.Model
{
    public class InventoryItemStorageDimensionModel : BaseModel
    {
        public InventoryItemStorageDimensionModel()
        {

        }
        public short InventoryItemStorageDimensionId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }      
    
    }
}
