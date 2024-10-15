namespace Coditech.Common.API.Model
{
    public class DBTMActivityCategoryModel : BaseModel
    {
        public short DBTMActivityCategoryId { get; set; }
        public int DBTMParentActivityCategoryId { get; set; }
        public string ActivityCategoryCode { get; set; }
        public string ActivityCategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
