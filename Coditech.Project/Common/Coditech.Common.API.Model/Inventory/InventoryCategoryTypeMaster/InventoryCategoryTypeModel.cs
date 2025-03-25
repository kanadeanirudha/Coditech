namespace Coditech.Common.API.Model
{
    public class InventoryCategoryTypeModel : BaseModel
    {
        public byte InventoryCategoryTypeMasterId { get; set; }
        public string CategoryTypeName { get; set; }
        public bool IsActive { get; set; }
    }
}

