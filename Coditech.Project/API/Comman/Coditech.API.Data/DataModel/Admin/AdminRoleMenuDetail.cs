namespace Coditech.API.Data
{
    public partial class AdminRoleMenuDetail : BaseDataModel
    {
        public int AdminRoleMenuDetailId { get; set; }
        public int AdminRoleMasterId { get; set; }
        public string AdminRoleCode { get; set; }
        public string MenuCode { get; set; }
        public Nullable<System.DateTime> EnableDate { get; set; }
        public Nullable<System.DateTime> DisableDate { get; set; }
        public string DisablePurpose { get; set; }
        public bool IsActive { get; set; }
    }
}
