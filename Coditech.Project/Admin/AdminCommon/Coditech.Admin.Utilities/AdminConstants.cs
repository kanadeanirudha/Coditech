namespace Coditech.Admin.Utilities
{
    public struct AdminConstants
    {
        public const string DESCKey = "DESC";
        public const string ASCKey = "ASC";
        public const string LoginCookieNameValue = "loginCookie";
        public const string AreaKey = "area";
        public const string Controller = "controller";
        public const string Action = "action";
        public const string UserDataSession = "UserData";
        public const string IsAdminUserSessionKey = "IsAdminUser";
        public const string LoginPath = "/User/login";
        public const string LogoutPath = "/User/logout";

        #region CookieHelper constant
        public const double MinutesInAYear = 365 * 24 * 60;
        public const double MinutesInADay = 24 * 60;
        public const double MinutesInAHour = 60;
        #endregion
    }
}
