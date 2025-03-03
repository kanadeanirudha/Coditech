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
        MedicalSpecialization,
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
        CallingCode,
        HospitalPathologyTestGroup,
        HospitalPathologyTestGroupParent,
        HospitalPathologyTestSampleTypes,
        GazetteChapter,
        PathologyPriceCategory,
        HospitalPathologyTestName,
        PathologyTestNameByPathologyPriceCategory,
        TrainerSpecialization,
        UnAssociatedTrainerList,
        TicketPriority,
        TicketStatus,
        PaymentGateway,
        AccSetupBalanceSheetType,
        UnAssociatedTrainerEmployeeList,
        AccSetupBalanceSheet,
        AccSetupGLDropdown,
        SchedulerFrequency,
        SchedulerType,
        SchedulerCallFor,
        SchedulerWeeks,
        AccSetupGLType,
        AccSetupTransactionType,
        CentrewiseAccountBalanceSheet,
        LimitedDataAccess,
        DashboardDaysDropDown
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
        DateFormatForCalendar,
    }

    public enum UserTypeEnum
    {
        Admin,
        Employee,
        Customer,
        GymMember,
        Patient,
        Trainee
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
        PatientUAHNumber
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
        ResetPasswordLink,
        AssociateGymMembershipPlan,
        SendOTP,
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
        UploadFile,
        DownloadFile,
        DeleteFile,
    }

    public enum GymFollowupTypesEnum
    {
        Call,
        TextSMS,
        WhatsAppSMS,
        BulkSMS,
        Visit
    }
    public enum HospitalApprovalStatusEnum
    {
        HospitalPending
    }
    public enum GeneralEnumaratorGroupCodeEnum
    {
        EmployeeStage,
        GeneralRunningNumberFor,
        InventoryProductType,
        DashboardForm,
        HospitalApprovalStatus,
        TicketStatus,
    }
    public enum PaymentCodeEnum
    {
        razorpay
    }
    public enum NarrationTypeEnum
    {
        Sales,
        Invoice,
        Service,
        Dividends,
        Salary,
        RoundOff,
        Exchange,
        Depreciation,
        Payable,
        Receivable,
        CostofGoodsSold,
        StockAdjustment,
        Tax,
        WriteOff,
        AccumulatedDepreciation,
        DeferredRevenue,
        OpeningBalanceEquity,
        ExchangeGainLoss,
        OfficeMaintenanceExpenses,
        PrintandStationery,
    }
    public enum ActionModeEnum
    {
        Create,
        Update,
        Delete,
    }
    public enum AccSetupChartOfAccountTemplateEnum
    {
        IndianStandard,
        Existing,
    }
    public enum SchedulerFrequencyEnum
    {
        OneTime,
        Daily,
        Weekly,
        //Monthlyum
    }
    public enum SchedulerTypeEnum
    {
        Scheduled,
        Runtime
    }
    public enum SchedulerCallForEnum
    {
       Batch
    }
}
