using Coditech.Common.Logger;
using Coditech.Common.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Coditech.API.Service.DependencyRegistration
{
    public static class DependencyRegistration
    {
        public static void RegisterDI(this WebApplicationBuilder builder)
        {
            // Add Dependency 
            builder.Services.AddScoped<ICoditechLogging, CoditechLogging>();
            builder.Services.AddScoped<IGeneralDepartmentMasterService, GeneralDepartmentMasterService>();
            builder.Services.AddScoped<IGeneralCountryMasterService, GeneralCountryMasterService>();
            builder.Services.AddScoped<IGeneralEmailTemplateService, GeneralEmailTemplateService>();
            builder.Services.AddScoped<IGeneralFinancialYearMasterService, GeneralFinancialYearMasterService>();
            builder.Services.AddScoped<IGeneralTaxMasterService, GeneralTaxMasterService>();
            builder.Services.AddScoped<IGeneralTaxGroupMasterService, GeneralTaxGroupMasterService>();
            builder.Services.AddScoped<IGeneralCityMasterService, GeneralCityMasterService>();
            builder.Services.AddScoped<IGeneralNationalityMasterService, GeneralNationalityMasterService>();
            builder.Services.AddScoped<IGeneralDesignationMasterService, GeneralDesignationMasterService>();
            builder.Services.AddScoped<IGeneralEnumaratorGroupService, GeneralEnumaratorGroupService>();
            builder.Services.AddScoped<IAdminSanctionPostService, AdminSanctionPostService>();
            builder.Services.AddScoped<IGeneralRegionMasterService, GeneralRegionMasterService>();
            builder.Services.AddScoped<IAdminRoleMasterService, AdminRoleMasterService>();
            builder.Services.AddScoped<IGeneralSystemGlobleSettingService, GeneralSystemGlobleSettingService>();
            builder.Services.AddScoped<IGeneralOccupationMasterService, GeneralOccupationMasterService>();
            builder.Services.AddScoped<IGeneralMeasurementUnitMasterService, GeneralMeasurementUnitMasterService>();
            builder.Services.AddScoped<IGeneralRunningNumbersService, GeneralRunningNumbersService>();
            builder.Services.AddScoped<IGeneralLeadGenerationMasterService, GeneralLeadGenerationMasterService>();
            builder.Services.AddScoped<IGeneralUserMainMenuMasterService, GeneralUserMainMenuMasterService>();
            builder.Services.AddScoped<ICoditechApplicationSettingService, CoditechApplicationSettingService>();
            builder.Services.AddScoped<IGeneralSmsProviderMasterService, GeneralSmsProviderService>();
            builder.Services.AddScoped<IGeneralWhatsAppProviderMasterService, GeneralWhatsAppProviderService>();
            builder.Services.AddScoped<IGeneralDistrictMasterService, GeneralDistrictMasterService>();
            builder.Services.AddScoped<IGeneralNotificationMasterService, GeneralNotificationService>();
            builder.Services.AddScoped<IGeneralTrainerMasterService, GeneralTrainerMasterService>();
            builder.Services.AddScoped<IGeneralBatchMasterService, GeneralBatchMasterService>();

            //Organisation
            builder.Services.AddScoped<IOrganisationMasterService, OrganisationMasterService>();
            builder.Services.AddScoped<IOrganisationCentreMasterService, OrganisationCentreMasterService>();
            builder.Services.AddScoped<IOrganisationCentrewiseDepartmentService, OrganisationCentrewiseDepartmentService>();
            builder.Services.AddScoped<IOrganisationCentrewiseBuildingMasterService, OrganisationCentrewiseBuildingMasterService>();
            builder.Services.AddScoped<IOrganisationCentrewiseBuildingRoomsService, OrganisationCentrewiseBuildingRoomsService>();
            builder.Services.AddScoped<IOrganisationCentrewiseJoiningCodeService, OrganisationCentrewiseJoiningCodeService>();

            builder.Services.AddScoped<ILogMessageService, LogMessageService>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICoditechEmail, CoditechEmail>();
            builder.Services.AddScoped<ICoditechSMS, CoditechSMS>();
            builder.Services.AddScoped<ICoditechWhatsApp, CoditechWhatsApp>();
            builder.Services.AddScoped<IGeneralPersonFollowUpService, GeneralPersonFollowUpService>();
            builder.Services.AddScoped<IGeneralPersonAttendanceDetailsService, GeneralPersonAttendanceDetailsService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IGeneralCommonService, GeneralCommonService>();

            #region Task Approval
            builder.Services.AddScoped<ITaskMasterService, TaskMasterService>();
            builder.Services.AddScoped<ITaskApprovalSettingService, TaskApprovalSettingService>();
            #endregion

            #region Ticket Master
            builder.Services.AddScoped<ITicketMasterService, TicketMasterService>();
            #endregion

            #region Employee
            builder.Services.AddScoped<IEmployeeMasterService, EmployeeMasterService>();
            builder.Services.AddScoped<IEmployeeServiceService, EmployeeServiceService>();
            #endregion

            #region Inventory
            builder.Services.AddScoped<IInventoryGeneralItemMasterService, InventoryGeneralItemMasterService>();
            builder.Services.AddScoped<IInventoryCategoryService, InventoryCategoryService>();
            builder.Services.AddScoped<IInventoryItemModelGroupService, InventoryItemModelGroupService>();
            builder.Services.AddScoped<IInventoryProductDimensionGroupService, InventoryProductDimensionGroupService>();
            builder.Services.AddScoped<IInventoryItemStorageDimensionService, InventoryItemStorageDimensionService>();
            builder.Services.AddScoped<IInventoryItemTrackingDimensionService, InventoryItemTrackingDimensionService>();
            builder.Services.AddScoped<IInventoryProductDimensionService, InventoryProductDimensionService>();
            builder.Services.AddScoped<IInventoryItemGroupService, InventoryItemGroupService>();
            builder.Services.AddScoped<IInventoryUoMMasterService, InventoryUoMMasterService>();
            builder.Services.AddScoped<IInventoryStorageDimensionGroupService, InventoryStorageDimensionGroupService>();
            builder.Services.AddScoped<IInventoryItemTrackingDimensionGroupService, InventoryItemTrackingDimensionGroupService>();
            #endregion

            #region Accounts
            builder.Services.AddScoped<IAccGLSetupNarrationService, AccGLSetupNarrationService>();
            builder.Services.AddScoped<IAccSetupMasterService, AccSetupMasterService>();
            builder.Services.AddScoped<IAccSetupBalanceSheetService, AccSetupBalanceSheetService>();
            builder.Services.AddScoped<IAccSetupBalanceSheetTypeService, AccSetupBalanceSheetTypeService>();
            #endregion

        }
    }
}
