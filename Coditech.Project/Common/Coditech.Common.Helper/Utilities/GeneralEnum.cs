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
        District,
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
        SMSTemplate,
        WhatsAppTemplate,
        HospitalDoctorsList,
        HospitalAppointmentType,
        HospitalDoctorTimeSlot,
        InventoryProductType,
        ProductSubType,
        InventoryProductDimensionGroup,
        InventoryItemGroup,
        InventoryStorageDimensionGroup,
        InventoryItemTrackingDimensionGroup,
        EmployeeStage,
        InventoryUomMaster,
        ReportType,
        SMSProvider,
        WhatsAppProvider,
        HospitalPatientType,
        HospitalDoctorsListBySpecialization,
        HospitalPatientAppointmentPurpose,
        CentrewiseHospitalPatientsList,
        TimeSlotByDoctorsListAndAppointmentDate,
        CallingCode
    }

    public enum GeneralSystemGlobleSettingEnum
    {
        GSTEInvoiceCancellationPeriodInMinute,
        CoditechModules,
        IsGymMemberLogin,
        IsEmployeeLogin,
        IsPatientLogin,
        ActiveProjectName,
        DefaultPassword,
        DefaultCultureName,
        PriceRoundOff,
        InventoryRoundOff,
        DateFormat,
        DateFormatForCalendar
    }

    public enum UserTypeEnum
    {
        Admin,
        Employee,
        Customer,
        GymMember,
        Patient
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
        InvoiceNumber,
        PatientUAHNumber,
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
        GymMemberRegistration,
        ResetPasswordLink
    }
    public enum UserNameRegistrationTypeEnum
    {
        EmailId,
        MobileNumber,
        PersonCode
    }
    public enum UploadStatusCodeEnum
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

    public enum MediaFolderActionEnum
    {
        ViewFolder,
        CreateFolder,
        RenameFolder,
        MoveFolder,
        DeleteFolder,
        DeleteFile,
    }
}
