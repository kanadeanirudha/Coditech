﻿namespace Coditech.Common.Helper.Utilities
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
        public static string MembershipPlanName { get; } = "#MembershipPlanName";
        public static string PlanDurationType { get; } = "#PlanDurationType#";
        public static string PaymentMethod { get; } = "#PaymentMethod#"; 
        public static string PaidAmount { get; } = "#PaidAmount#";
        public static string PlanDuration { get; } = "#PlanDuration#";
        public static string PersonCode { get; } = "#PersonCode#";
        public static string Designation { get; } = "#Designation#";
        public static string DepartmentName { get; } = "#DepartmentName#";
    }
}
