﻿namespace Coditech.Admin.Utilities
{
    public struct AdminConstants
    {
        public const string DESCKey = "desc";
        public const string ASCKey = "asc";
        public const string LoginCookieNameValue = "loginCookie";
        public const string LogoCookieNameValue = "logoCookie";
        public const string AreaKey = "area";
        public const string Controller = "controller";
        public const string Action = "action";
        public const string UserDataSession = "UserData";
        public const string IsAdminUserSessionKey = "IsAdminUser";
        public const string LoginPath = "/User/login";
        public const string LogoutPath = "/User/logout";
        public const string Notifications = "Notifications";
        public const string DataTableViewModel = "DataTableViewModel";
        public const string Self = "Self";
        public const string Other = "Other";
        public const string Regular = "Regular";
        public const string Addon = "Addon";
        public const string Temporary = "Temporary";
        public const string Permanent = "Permanent";
        public const string General = "General";
        public const string Centrewise = "Centrewise";
        public const string AccountPrerequisiteSession = "AccountUserData";

        #region CookieHelper constant
        public const double MinutesInAYear = 365 * 24 * 60;
        public const double MinutesInADay = 24 * 60;
        public const double MinutesInAHour = 60;
        public const string PlanDurationType = "duration";
        public const string PlanSessionType = "session";
        #endregion
    }
}
