using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class DBTMActivityCategoryViewModel : BaseViewModel
    {
        public short DBTMActivityCategoryId { get; set; }
        public int DBTMParentActivityCategoryId { get; set; }
        public string ActivityCategoryCode { get; set; }
        public string ActivityCategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
