﻿namespace Coditech.Common.Helper.Utilities
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
        InventoryModel,
        EmailTemplate,
        HospitalDoctorsList
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
    public enum GeneralRunningNumberForEnum
    {
        GymMemberRegistration,
        EmployeeRegistration,
        InvoiceNumber
    }
    public enum DashboardFormEnum
    {
        GymOwnerDashboard,
        GymOperatorDashboard
    }

    public enum ErrorMessageTypeEnum
    {
        Application
    }
    // Specifies the text file access modes.
    public enum FileModeEnum
    {
        // Indicates text file read mode operation.
        Read,

        // Indicates text file write mode operation. It deletes the previous content.
        Write,

        // Indicates text file append mode operation. it preserves the previous content.
        Append
    }
    public enum EmailTemplateCodeEnum
    {
        EmployeeRegistration,
        CustomerRegistration,
        GymMemberRegistration
    }
    public enum UserNameRegistrationType
    {
        EmailId,
        MobileNumber,
        PersonCode
    }
    public enum UploadStatusCode
    {
        ExtensionNotAllow = 10,
        FileAlreadyExist = 20,
        MaxFileSize = 30,
        Corrupt = 40,
        Error = 50,
        Done = 60,
        Removed = 70,
        SelectSingleFile = 80,
        SelectFile = 90,
        UnSupportedFile = 100
    }
}
