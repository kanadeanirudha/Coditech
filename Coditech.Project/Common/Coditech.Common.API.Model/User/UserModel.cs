namespace Coditech.Common.API.Model
{
    public class UserModel : BaseModel
    {
        public UserModel()
        {
            RoleList = new List<AdminRoleDetailsModel>();
            ModuleList = new List<UserModuleModel>();
            MenuList = new List<UserMenuModel>();
            BalanceSheetList = new List<UserBalanceSheetModel>();
            AccessibleCentreList = new List<UserAccessibleCentreModel>();
            CountryList = new List<UserCountryModel>();
        }
        public int UserMasterId { get; set; }
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
        public int SelectedRoleId { get; set; }
        public string SelectedRoleCode { get; set; }
        public string SelectedBalanceSheet { get; set; }
        public int SelectedBalanceId { get; set; }
        public string SelectedCentreCode { get; set; } = string.Empty;
        public string GeneralCountryMasterId { get; set; } 
        public List<AdminRoleDetailsModel> RoleList { get; set; }
        public List<UserModuleModel> ModuleList { get; set; }
        public List<UserMenuModel> MenuList { get; set; }
        public List<UserBalanceSheetModel> BalanceSheetList { get; set; }
        public List<UserAccessibleCentreModel> AccessibleCentreList { get; set; }
        public List<UserCountryModel> CountryList { get; set; }
    }
}
