namespace Coditech.Common.API.Model
{
    public partial class AccSetupCategoryModel : BaseModel
    {
        public short AccSetupCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemGenerated { get; set; }
    }
}
