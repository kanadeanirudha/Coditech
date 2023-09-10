namespace Coditech.Common.API.Model
{
    public class AdminRoleDetailsModel : BaseModel
    {
        public int AdminRoleMasterId { get; set; }
        public string AdminRoleCode { get; set; }
        public string RoleType { get; set; }
    }
}
