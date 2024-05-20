namespace Coditech.Common.API.Model
{
    public class InventoryItemGroupModel : BaseModel
    {
        public Int16 InventoryItemGroupId { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemGroupCode { get; set; }
        public bool ConsiderInProdReport { get; set; }
    }
}
