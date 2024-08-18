namespace Coditech.Common.Helper.Utilities
{
    public struct  EmailTemplateTokenConstant
    {
        public static string CentreName { get; } = "#CentreName#";
        public static string CentreAddress { get; } = "#CentreAddress#";
        public static string CentreContactNumber { get; } = "#CentreContactNumber#";
        public static string FirstName { get; } = "#FirstName#";
        public static string LastName { get; } = "#LastName#";
        public static string Url { get; } = "#Url#";
        public static string OTP { get; } = "#OTP#";
        public static string CentreUrl { get; } = "#CentreUrl#";
        public static string EmployeeUsername { get; } = "#EmployeeUsername#";
        public static string TemporaryPassword { get; } = "#TemporaryPassword#";
    }
}
