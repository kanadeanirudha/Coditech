namespace Coditech.Common.Helper.Utilities
{
    public enum DropdownTypeEnum
    {
        City,
        AccessibleCentre,
        CentrewiseDepartment,
        CentrewiseBuilding,
        Department,
        Designation,
        Organisation,
        RegionalOffice,
        Centre,
        TaxGroup,
        Country,
        Region,
        ModuleList,
        MenuList,
        Nationality,
        Gender,
        IndentificationType,
        MaritalStatus,
        BloodGroups,
        Title,
        Occupation,
        GymGroup,
        GymSource,
        MeasurementUnit,
        UnAssociatedEmployeeList,
        GymPlanType,
        GymPlanDurationType,
        FinancialYear,
        GeneralRunningNumberFor,
        GymFollowupTypes,
        LeadStatus,
        LeadCategory,
        UserType,
        MedicalSpecilization,
        WeekDays,
        CentrewiseBuildingRooms,
        Floors,
        AttendanceState,
        GymMembershipPlan,
        PaymentType,
        InventoryGeneralServiecs,
        DashboardForm,
        InventoryCategory,
        AllCities,
        InventoryModel
    }

    public enum GeneralSystemGlobleSettingEnum
    {
        GSTEInvoiceCancellationPeriodInMinute,
        CoditechModules,
        IsGymMemberLogin,
        IsEmployeeLogin,
        ActiveProjectName,
        DefaultPassword,
        DefaultCultureName,
        PriceRoundOff,
        InventoryRoundOff,
        DateFormat,
    }

    public enum UserTypeEnum
    {
        Admin,
        Employee,
        Customer,
        GymMember
    }

    public enum ActiveProjectNameEnum
    {
        GMS,
        HMS
    }

    public enum MediaTypeEnum
    {
        Image,
        File,
        Video,
        Audio
    }

    public enum AddressTypeEnum
    {
        PermanentAddress,
        CorrespondanceAddress,
        BusinessAddress        
    }
    public enum GeneralRunningNumberFor
    {
        GymMemberRegistration,
        EmployeeRegistration,
        InvoiceNumber
    }
}
