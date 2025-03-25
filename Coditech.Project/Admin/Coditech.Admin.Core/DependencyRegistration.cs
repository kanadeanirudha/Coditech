using Coditech.Admin.Agents;
using Coditech.Admin.Helpers;
using Coditech.API.Client;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace Coditech.Admin
{
    public static class DependencyRegistration
    {
        public static void RegisterDI(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<CoditechTranslator>();

            #region Agent
            #region Admin
            builder.Services.AddScoped<IAdminSanctionPostAgent, AdminSanctionPostAgent>();
            builder.Services.AddScoped<IAdminRoleMasterAgent, AdminRoleMasterAgent>();
            #endregion

            builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            builder.Services.AddSingleton<ICoditechLogging, CoditechLogging>();
            builder.Services.AddScoped<IUserAgent, UserAgent>();
            builder.Services.AddScoped<IGeneralDepartmentAgent, GeneralDepartmentAgent>();
            builder.Services.AddScoped<IGeneralCountryAgent, GeneralCountryAgent>();
            builder.Services.AddScoped<IGeneralEmailTemplateAgent, GeneralEmailTemplateAgent>();
            builder.Services.AddScoped<IGeneralFinancialYearAgent, GeneralFinancialYearAgent>();
            builder.Services.AddScoped<IGeneralNationalityAgent, GeneralNationalityAgent>();
            builder.Services.AddScoped<IGeneralDesignationAgent, GeneralDesignationAgent>();
            builder.Services.AddScoped<IGeneralCityAgent, GeneralCityAgent>();
            builder.Services.AddScoped<IGeneralTaxMasterAgent, GeneralTaxMasterAgent>();
            builder.Services.AddScoped<IGeneralTaxGroupAgent, GeneralTaxGroupAgent>();
            builder.Services.AddScoped<IOrganisationAgent, OrganisationAgent>();
            builder.Services.AddScoped<IOrganisationCentreAgent, OrganisationCentreAgent>();
            builder.Services.AddScoped<IOrganisationCentrewiseBuildingAgent, OrganisationCentrewiseBuildingAgent>();
            builder.Services.AddScoped<IGeneralRegionAgent, GeneralRegionAgent>();
            builder.Services.AddScoped<IGeneralSystemGlobleSettingAgent, GeneralSystemGlobleSettingAgent>();
            builder.Services.AddScoped<IEmployeeMasterAgent, EmployeeMasterAgent>();
            builder.Services.AddScoped<IGeneralEnumaratorGroupAgent, GeneralEnumaratorGroupAgent>();
            builder.Services.AddScoped<IGeneralOccupationAgent, GeneralOccupationAgent>();
            builder.Services.AddScoped<IGeneralMeasurementUnitAgent, GeneralMeasurementUnitAgent>();
            builder.Services.AddScoped<IOrganisationCentrewiseDepartmentAgent, OrganisationCentrewiseDepartmentAgent>();
            builder.Services.AddScoped<IGeneralRunningNumbersAgent, GeneralRunningNumbersAgent>();
            builder.Services.AddScoped<IGeneralLeadGenerationAgent, GeneralLeadGenerationAgent>();
            builder.Services.AddScoped<IOrganisationCentrewiseBuildingRoomsAgent, OrganisationCentrewiseBuildingRoomsAgent>();
            builder.Services.AddScoped<IGeneralPersonFollowUpAgent, GeneralPersonFollowUpAgent>();
            builder.Services.AddScoped<IDashboardAgent, DashboardAgent>();
            builder.Services.AddScoped<IInventoryItemTrackingDimensionAgent, InventoryItemTrackingDimensionAgent>();
            builder.Services.AddScoped<IInventoryProductDimensionAgent, InventoryProductDimensionAgent>();
            builder.Services.AddScoped<IGeneralUserMainMenuAgent, GeneralUserMainMenuAgent>();
            builder.Services.AddScoped<IEmployeeServiceAgent, EmployeeServiceAgent>();
            builder.Services.AddScoped<IGeneralCommonAgent, GeneralCommonAgent>();
            builder.Services.AddScoped<ICoditechApplicationSettingAgent, CoditechApplicationSettingAgent>();
            builder.Services.AddScoped<IMediaManagerFolderAgent, MediaManagerFolderAgent>();
            builder.Services.AddScoped<ILogMessageAgent, LogMessageAgent>();
            builder.Services.AddScoped<IGeneralSmsProviderAgent, GeneralSmsProviderAgent>();
            builder.Services.AddScoped<IGeneralWhatsAppProviderAgent, GeneralWhatsAppProviderAgent>();
            builder.Services.AddScoped<IGeneralDistrictAgent, GeneralDistrictAgent>();
            builder.Services.AddScoped<IGeneralNotificationAgent, GeneralNotificationAgent>();
            builder.Services.AddScoped<IGeneralTrainerAgent, GeneralTrainerAgent>();
            builder.Services.AddScoped<IGeneralBatchAgent, GeneralBatchAgent>();
            builder.Services.AddScoped<ITaskApprovalSettingAgent, TaskApprovalSettingAgent>();
            builder.Services.AddScoped<ITaskMasterAgent, TaskMasterAgent>();
            builder.Services.AddScoped<ITicketMasterAgent, TicketMasterAgent>();
            builder.Services.AddScoped<IOrganisationCentrewiseJoiningCodeAgent, OrganisationCentrewiseJoiningCodeAgent>();
            builder.Services.AddScoped<IGeneralCurrencyMasterAgent, GeneralCurrencyMasterAgent>();
            builder.Services.AddScoped<IGeneralTaskSchedulerMasterAgent, GeneralTaskSchedulerMasterAgent>();

            #region HMS
            builder.Services.AddScoped<IHospitalDoctorsAgent, HospitalDoctorsAgent>();
            builder.Services.AddScoped<IHospitalDoctorVisitingChargesAgent, HospitalDoctorVisitingChargesAgent>();
            builder.Services.AddScoped<IHospitalDoctorAllocatedOPDRoomAgent, HospitalDoctorAllocatedOPDRoomAgent>();
            builder.Services.AddScoped<IHospitalDoctorLeaveScheduleAgent, HospitalDoctorLeaveScheduleAgent>();
            builder.Services.AddScoped<IHospitalPatientRegistrationAgent, HospitalPatientRegistrationAgent>();
            builder.Services.AddScoped<IHospitalDoctorOPDScheduleAgent, HospitalDoctorOPDScheduleAgent>();
            builder.Services.AddScoped<IHospitalPatientAppointmentPurposeAgent, HospitalPatientAppointmentPurposeAgent>();
            builder.Services.AddScoped<IHospitalPatientTypeAgent, HospitalPatientTypeAgent>();
            builder.Services.AddScoped<IHospitalPatientAppointmentAgent, HospitalPatientAppointmentAgent>();
            builder.Services.AddScoped<IHospitalPathologyTestGroupAgent, HospitalPathologyTestGroupAgent>();
            builder.Services.AddScoped<IHospitalPathologyTestAgent, HospitalPathologyTestAgent>();
            builder.Services.AddScoped<IHospitalPathologyTestPricesAgent, HospitalPathologyTestPricesAgent>();
            builder.Services.AddScoped<IHospitalRegistrationFeeAgent, HospitalRegistrationFeeAgent>();
            #endregion

            #region MediaManager
            builder.Services.AddScoped<IMediaSettingMasterAgent, MediaSettingMasterAgent>();
            #endregion

            #region Inventory
            builder.Services.AddScoped<IInventoryProductDimensionGroupAgent, InventoryProductDimensionGroupAgent>();
            builder.Services.AddScoped<IInventoryItemModelGroupAgent, InventoryItemModelGroupAgent>();
            builder.Services.AddScoped<IInventoryCategoryAgent, InventoryCategoryAgent>();
            builder.Services.AddScoped<IInventoryItemTrackingDimensionAgent, InventoryItemTrackingDimensionAgent>();
            builder.Services.AddScoped<IInventoryProductDimensionAgent, InventoryProductDimensionAgent>();
            builder.Services.AddScoped<IInventoryItemStorageDimensionAgent, InventoryItemStorageDimensionAgent>();
            builder.Services.AddScoped<IInventoryItemGroupAgent, InventoryItemGroupAgent>();
            builder.Services.AddScoped<IInventoryUoMMasterAgent, InventoryUoMMasterAgent>();
            builder.Services.AddScoped<IInventoryStorageDimensionGroupAgent, InventoryStorageDimensionGroupAgent>();
            builder.Services.AddScoped<IInventoryItemTrackingDimensionGroupAgent, InventoryItemTrackingDimensionGroupAgent>();
            builder.Services.AddScoped<IInventoryGeneralItemMasterAgent, InventoryGeneralItemMasterAgent>();
            builder.Services.AddScoped<IInventoryCategoryTypeAgent, InventoryCategoryTypeAgent>();
            #endregion

            #region Gazette
            builder.Services.AddScoped<IGazetteChaptersAgent, GazetteChaptersAgent>();
            builder.Services.AddScoped<IGazetteChaptersPageDetailAgent, GazetteChaptersPageDetailAgent>();
            #endregion

            #region Payment
            builder.Services.AddScoped<IPaymentGatewaysAgent, PaymentGatewaysAgent>();
            builder.Services.AddScoped<IPaymentGatewayDetailsAgent, PaymentGatewayDetailsAgent>();

            #endregion

            #region Accounts         
            builder.Services.AddScoped<IAccGLSetupNarrationAgent, AccGLSetupNarrationAgent>();
            builder.Services.AddScoped<IAccSetupMasterAgent, AccSetupMasterAgent>();
            builder.Services.AddScoped<IAccSetupBalanceSheetAgent, AccSetupBalanceSheetAgent>();
            builder.Services.AddScoped<IAccSetupBalanceSheetTypeAgent, AccSetupBalanceSheetTypeAgent>();
            builder.Services.AddScoped<IAccSetupTransactionTypeAgent, AccSetupTransactionTypeAgent>();
            builder.Services.AddScoped<IAccSetupGLBankAgent, AccSetupGLBankAgent>();
            builder.Services.AddScoped<IAccSetupGLAgent, AccSetupGLAgent>();
            builder.Services.AddScoped<IAccSetupChartOfAccountTemplateAgent, AccSetupChartOfAccountTemplateAgent>();
            builder.Services.AddScoped<IAccGLTransactionAgent, AccGLTransactionAgent>();
            #endregion

            #endregion Agent

            #region Client
            #region Admin
            builder.Services.AddScoped<IAdminSanctionPostClient, AdminSanctionPostClient>();
            builder.Services.AddScoped<IAdminRoleMasterClient, AdminRoleMasterClient>();
            #endregion Admin

            builder.Services.AddScoped<IUserClient, UserClient>();
            builder.Services.AddScoped<IGeneralDepartmentClient, GeneralDepartmentClient>();
            builder.Services.AddScoped<IGeneralCountryClient, GeneralCountryClient>();
            builder.Services.AddScoped<IGeneralEmailTemplateClient, GeneralEmailTemplateClient>();
            builder.Services.AddScoped<IGeneralFinancialYearClient, GeneralFinancialYearClient>();
            builder.Services.AddScoped<IGeneralNationalityClient, GeneralNationalityClient>();
            builder.Services.AddScoped<IGeneralDesignationClient, GeneralDesignationClient>();
            builder.Services.AddScoped<IGeneralCityClient, GeneralCityClient>();
            builder.Services.AddScoped<IGeneralTaxMasterClient, GeneralTaxMasterClient>();
            builder.Services.AddScoped<IGeneralTaxGroupClient, GeneralTaxGroupClient>();
            builder.Services.AddScoped<IOrganisationClient, OrganisationClient>();
            builder.Services.AddScoped<IOrganisationCentreClient, OrganisationCentreClient>();
            builder.Services.AddScoped<IOrganisationCentrewiseBuildingClient, OrganisationCentrewiseBuildingClient>();
            builder.Services.AddScoped<IGeneralRegionClient, GeneralRegionClient>();
            builder.Services.AddScoped<IGeneralSystemGlobleSettingClient, GeneralSystemGlobleSettingClient>();
            builder.Services.AddScoped<IEmployeeMasterClient, EmployeeMasterClient>();
            builder.Services.AddScoped<IMediaManagerClient, MediaManagerClient>();
            builder.Services.AddScoped<IGeneralEnumaratorGroupClient, GeneralEnumaratorGroupClient>();
            builder.Services.AddScoped<IGeneralOccupationClient, GeneralOccupationClient>();
            builder.Services.AddScoped<IGeneralMeasurementUnitClient, GeneralMeasurementUnitClient>();
            builder.Services.AddScoped<IOrganisationCentrewiseDepartmentClient, OrganisationCentrewiseDepartmentClient>();
            builder.Services.AddScoped<IGeneralRunningNumbersClient, GeneralRunningNumbersClient>();
            builder.Services.AddScoped<IGeneralLeadGenerationClient, GeneralLeadGenerationClient>();
            builder.Services.AddScoped<IOrganisationCentrewiseBuildingRoomsClient, OrganisationCentrewiseBuildingRoomsClient>();
            builder.Services.AddScoped<IGeneralPersonFollowUpClient, GeneralPersonFollowUpClient>();
            builder.Services.AddScoped<IGeneralPersonAttendanceDetailsClient, GeneralPersonAttendanceDetailsClient>();
            builder.Services.AddScoped<IInventoryGeneralItemMasterClient, InventoryGeneralItemMasterClient>();
            builder.Services.AddScoped<IInventoryCategoryTypeClient, InventoryCategoryTypeClient>();
            builder.Services.AddScoped<IInventoryCategoryClient, InventoryCategoryClient>();
            builder.Services.AddScoped<IDashboardClient, DashboardClient>();
            builder.Services.AddScoped<IInventoryItemModelGroupClient, InventoryItemModelGroupClient>();
            builder.Services.AddScoped<IInventoryItemStorageDimensionClient, InventoryItemStorageDimensionClient>();
            builder.Services.AddScoped<IInventoryItemTrackingDimensionClient, InventoryItemTrackingDimensionClient>();
            builder.Services.AddScoped<IInventoryProductDimensionClient, InventoryProductDimensionClient>();
            builder.Services.AddScoped<IInventoryProductDimensionGroupClient, InventoryProductDimensionGroupClient>();
            builder.Services.AddScoped<IInventoryItemGroupClient, InventoryItemGroupClient>();
            builder.Services.AddScoped<IInventoryUoMMasterClient, InventoryUoMMasterClient>();
            builder.Services.AddScoped<IGeneralUserMainMenuClient, GeneralUserMainMenuClient>();
            builder.Services.AddScoped<IInventoryStorageDimensionGroupClient, InventoryStorageDimensionGroupClient>();
            builder.Services.AddScoped<IInventoryItemTrackingDimensionGroupClient, InventoryItemTrackingDimensionGroupClient>();
            builder.Services.AddScoped<IEmployeeServiceClient, EmployeeServiceClient>();
            builder.Services.AddScoped<ICoditechApplicationSettingClient, CoditechApplicationSettingClient>();
            builder.Services.AddScoped<ILogMessageClient, LogMessageClient>();
            builder.Services.AddScoped<IGeneralSmsProviderClient, GeneralSmsProviderClient>();
            builder.Services.AddScoped<IGeneralWhatsAppProviderClient, GeneralWhatsAppProviderClient>();
            builder.Services.AddScoped<IGeneralDistrictClient, GeneralDistrictClient>();
            builder.Services.AddScoped<IGeneralNotificationClient, GeneralNotificationClient>();
            builder.Services.AddScoped<IGeneralTrainerClient, GeneralTrainerClient>();
            builder.Services.AddScoped<IGeneralBatchClient, GeneralBatchClient>();
            builder.Services.AddScoped<ITaskApprovalSettingClient, TaskApprovalSettingClient>();
            builder.Services.AddScoped<ITaskMasterClient, TaskMasterClient>();
            builder.Services.AddScoped<IGeneralCommonClient, GeneralCommonClient>();
            builder.Services.AddScoped<ITicketMasterClient, TicketMasterClient>();
            builder.Services.AddScoped<IOrganisationCentrewiseJoiningCodeClient, OrganisationCentrewiseJoiningCodeClient>();
            builder.Services.AddScoped<IGeneralCurrencyMasterClient, GeneralCurrencyMasterClient>();
            builder.Services.AddScoped<ITaskSchedulerClient, TaskSchedulerClient>();
            

            #region HMS
            builder.Services.AddScoped<IHospitalDoctorsClient, HospitalDoctorsClient>();
            builder.Services.AddScoped<IHospitalDoctorVisitingChargesClient, HospitalDoctorVisitingChargesClient>();
            builder.Services.AddScoped<IHospitalDoctorAllocatedOPDRoomClient, HospitalDoctorAllocatedOPDRoomClient>();
            builder.Services.AddScoped<IHospitalDoctorLeaveScheduleClient, HospitalDoctorLeaveScheduleClient>();
            builder.Services.AddScoped<IHospitalPatientRegistrationClient, HospitalPatientRegistrationClient>();
            builder.Services.AddScoped<IHospitalDoctorOPDScheduleClient, HospitalDoctorOPDScheduleClient>();
            builder.Services.AddScoped<IHospitalPatientTypeClient, HospitalPatientTypeClient>();
            builder.Services.AddScoped<IHospitalPatientAppointmentPurposeClient, HospitalPatientAppointmentPurposeClient>();
            builder.Services.AddScoped<IHospitalPatientAppointmentClient, HospitalPatientAppointmentClient>();
            builder.Services.AddScoped<IHospitalPathologyTestGroupClient, HospitalPathologyTestGroupClient>();
            builder.Services.AddScoped<IHospitalPathologyTestClient, HospitalPathologyTestClient>();
            builder.Services.AddScoped<IHospitalPathologyTestPricesClient, HospitalPathologyTestPricesClient>();
            builder.Services.AddScoped<IHospitalRegistrationFeeClient, HospitalRegistrationFeeClient>();
            #endregion HMS

            #region MediaManager            
            builder.Services.AddScoped<IMediaSettingMasterClient, MediaSettingMasterClient>();
            #endregion HMS

            #region Gazette          
            builder.Services.AddScoped<IGazetteChaptersClient, GazetteChaptersClient>();
            builder.Services.AddScoped<IGazetteChaptersPageDetailClient, GazetteChaptersPageDetailClient>();
            #endregion Gazette

            #region Payment 
            builder.Services.AddScoped<IPaymentGatewaysClient, PaymentGatewaysClient>(); 
            builder.Services.AddScoped<IPaymentGatewayDetailsClient, PaymentGatewayDetailsClient>();
            #endregion

            #region Accounts
            builder.Services.AddScoped<IAccGLSetupNarrationClient, AccGLSetupNarrationClient>();
            builder.Services.AddScoped<IAccSetupMasterClient, AccSetupMasterClient>();
            builder.Services.AddScoped<IAccSetupBalanceSheetClient, AccSetupBalanceSheetClient>();
            builder.Services.AddScoped<IAccSetupBalanceSheetTypeClient, AccSetupBalanceSheetTypeClient>();
            builder.Services.AddScoped<IAccSetupTransactionTypeClient, AccSetupTransactionTypeClient>();
            builder.Services.AddScoped<IAccSetupGLBankClient, AccSetupGLBankClient>();
            builder.Services.AddScoped<IAccSetupGLClient, AccSetupGLClient>();
            builder.Services.AddScoped<IAccSetupChartOfAccountTemplateClient, AccSetupChartOfAccountTemplateClient>();
            builder.Services.AddScoped<IAccGLTransactionClient, AccGLTransactionClient>();
            #endregion

            #endregion Client
        }
    }
}
