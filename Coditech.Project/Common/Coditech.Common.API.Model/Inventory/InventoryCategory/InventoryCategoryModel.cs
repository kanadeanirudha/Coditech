namespace Coditech.Common.API.Model
{
    public class InventoryCategoryModel : BaseModel
    {
        public InventoryCategoryModel()
        {

        }
        public short InventoryCategoryId { get; set; }
        public short ParentInventoryCategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string ItemPrefix { get; set; }
        public byte InventoryCategoryTypeMasterId { get; set; }
    }
}
