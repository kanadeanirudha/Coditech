namespace Coditech.Common.API.Model
{
    public class UserModel : BaseModel
    {
        public UserModel()
        {
            RoleList = new List<AdminRoleDetailsModel>();
            ModuleList = new List<UserModuleModel>();
            MenuList = new List<UserMainMenuModel>();
            BalanceSheetList = new List<UserBalanceSheetModel>();
            AccessibleCentreList = new List<UserAccessibleCentreModel>();
        }
        public long UserMasterId { get; set; }
        public long EntityId { get; set; }
        public bool IsAdminUser { get; set; }
        public short UserTypeID { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public string DeviceToken { get; set; }
        public string LastModuleCode { get; set; }
        public int SelectedAdminRoleMasterId { get; set; }
        public string SelectedRoleCode { get; set; }
        public string SelectedBalanceSheet { get; set; }
        public int SelectedBalanceId { get; set; }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public List<AdminRoleDetailsModel> RoleList { get; set; }
        public List<UserModuleModel> ModuleList { get; set; }
        public List<UserMainMenuModel> MenuList { get; set; }
        public List<UserBalanceSheetModel> BalanceSheetList { get; set; }
        public List<UserAccessibleCentreModel> AccessibleCentreList { get; set; }
        public List<GeneralEnumaratorModel> GeneralEnumaratorList { get; set; }
        public List<GeneralSystemGlobleSettingModel> GeneralSystemGlobleSettingList { get; set; }
    }
}
