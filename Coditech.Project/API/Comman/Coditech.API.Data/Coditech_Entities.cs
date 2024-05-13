﻿using Coditech.API.Data.DataModel.Gym;
using Coditech.API.Data.DataModel.Inventory;
using Microsoft.EntityFrameworkCore;

namespace Coditech.API.Data
{
    public partial class Coditech_Entities : CoditechDbContext
    {
        public Coditech_Entities()
        {
        }

        public Coditech_Entities(DbContextOptions<Coditech_Entities> options) : base(options)
        {
        }

        #region General Master
        public DbSet<GeneralDepartmentMaster> GeneralDepartmentMaster { get; set; }
        public DbSet<GeneralCountryMaster> GeneralCountryMaster { get; set; }
        public DbSet<GeneralEmailTemplate> GeneralEmailTemplate { get; set; }
        public DbSet<GeneralFinancialYear> GeneralFinancialYear { get; set; }
        public DbSet<GeneralTaxMaster> GeneralTaxMaster { get; set; }
        public DbSet<GeneralTaxGroupMaster> GeneralTaxGroupMaster { get; set; }
        public DbSet<GeneralTaxGroupMasterDetails> GeneralTaxGroupMasterDetails { get; set; }
        public DbSet<GeneralCityMaster> GeneralCityMaster { get; set; }
        public DbSet<GeneralNationalityMaster> GeneralNationalityMaster { get; set; }
        public DbSet<EmployeeDesignationMaster> EmployeeDesignationMaster { get; set; }
        public DbSet<GeneralRegionMaster> GeneralRegionMaster { get; set; }
        public DbSet<GeneralEnumaratorMaster> GeneralEnumaratorMaster { get; set; }
        public DbSet<GeneralEnumaratorGroup> GeneralEnumaratorGroup { get; set; }
        public DbSet<GeneralSystemGlobleSettingMaster> GeneralSystemGlobleSettingMaster { get; set; }
        public DbSet<GeneralOccupationMaster> GeneralOccupationMaster { get; set; }
        public DbSet<GeneralMeasurementUnitMaster> GeneralMeasurementUnitMaster { get; set; }
        public DbSet<GeneralRunningNumbers> GeneralRunningNumbers { get; set; }
        public DbSet<GeneralLeadGenerationMaster> GeneralLeadGenerationMaster { get; set; }
        #endregion

        #region Organisation
        public DbSet<OrganisationMaster> OrganisationMaster { get; set; }
        public DbSet<OrganisationCentreMaster> OrganisationCentreMaster { get; set; }
        public DbSet<OrganisationCentrewiseBuildingMaster> OrganisationCentrewiseBuildingMaster { get; set; }
        public DbSet<OrganisationCentrewiseDepartment> OrganisationCentrewiseDepartment { get; set; }
        public DbSet<OrganisationCentrePrintingFormat> OrganisationCentrePrintingFormat { get; set; }
        public DbSet<OrganisationCentrewiseGSTCredential> OrganisationCentrewiseGSTCredential { get; set; }
        public DbSet<OrganisationCentrewiseBuildingRooms> OrganisationCentrewiseBuildingRooms { get; set; }
        public DbSet<OrganisationCentrewiseSmtpSetting> OrganisationCentrewiseSmtpSetting { get; set; }
        public DbSet<OrganisationCentrewiseEmailTemplate> OrganisationCentrewiseEmailTemplate { get; set; }
        public DbSet<OrganisationCentrewiseUserNameRegistration> OrganisationCentrewiseUserNameRegistration { get; set; }
        #endregion

        #region Admin
        public DbSet<AdminRoleApplicableDetails> AdminRoleApplicableDetails { get; set; }
        public DbSet<AdminRoleCentreRights> AdminRoleCentreRights { get; set; }
        public DbSet<AdminRoleMaster> AdminRoleMaster { get; set; }
        public DbSet<AdminRoleMenuDetails> AdminRoleMenuDetails { get; set; }
        public DbSet<AdminSanctionPost> AdminSanctionPost { get; set; }
        #endregion

        #region User
        public DbSet<UserMaster> UserMaster { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<UserModuleMaster> UserModuleMaster { get; set; }
        public DbSet<UserMainMenuMaster> UserMainMenuMaster { get; set; }
        public DbSet<GeneralPerson> GeneralPerson { get; set; }
        public DbSet<GeneralPersonAddress> GeneralPersonAddress { get; set; }
        public DbSet<LogMessage> LogMessage { get; set; }
        #endregion

        #region Employee
        public DbSet<EmployeeMaster> EmployeeMaster { get; set; }        
        #endregion

        #region Gym
        public DbSet<GymMemberDetails> GymMemberDetails { get; set; }
        public DbSet<GymMemberFollowUp> GymMemberFollowUp { get; set; }
        public DbSet<GymBodyMeasurementType> GymBodyMeasurementType { get; set; }
        public DbSet<GymMembershipPlan> GymMembershipPlan { get; set; }
        public DbSet<GymMemberBodyMeasurement> GymMemberBodyMeasurement { get; set; }
        public DbSet<GeneralPersonAttendanceDetails> GeneralPersonAttendanceDetails { get; set; }
        public DbSet<GymMemberMembershipPlan> GymMemberMembershipPlan { get; set; }
        public DbSet<GymMembershipPlanPackage> GymMembershipPlanPackage { get; set; }
        #endregion

        #region MediaManager
        public DbSet<MediaConfiguration> MediaConfiguration { get; set; }
        public DbSet<MediaDetail> MediaDetail { get; set; }
        public DbSet<MediaFolderMaster> MediaFolderMaster { get; set; }
        public DbSet<MediaServerMaster> MediaServerMaster { get; set; }
        public DbSet<MediaSettingMaster> MediaSettingMaster { get; set; }
        public DbSet<MediaTypeExtensionMaster> MediaTypeExtensionMaster { get; set; }
        public DbSet<MediaTypeMaster> MediaTypeMaster { get; set; }
        #endregion

        #region HMS
        public DbSet<HospitalDoctors> HospitalDoctors { get; set; }
        public DbSet<HospitalDoctorVisitingCharges> HospitalDoctorVisitingCharges { get; set; }
        public DbSet<HospitalDoctorAllocatedRoom> HospitalDoctorAllocatedRoom { get; set; }
        public DbSet<HospitalDoctorLeaveSchedule> HospitalDoctorLeaveSchedule { get; set; }
        #endregion

        #region GeneralPerson
        public DbSet<GeneralPersonFollowUp> GeneralPersonFollowUp { get; set; }
        #endregion

        #region SalesAndPurchase
        public DbSet<SalesInvoiceDetails> SalesInvoiceDetails { get; set; }
        public DbSet<SalesInvoiceMaster> SalesInvoiceMaster { get; set; }
        #endregion

        #region Inventory
        public DbSet<InventoryCategory> InventoryCategory { get; set; }
        public DbSet<InventoryItemStorageDimension> InventoryItemStorageDimension { get; set; }
        public DbSet<InventoryGeneralItemMaster> InventoryGeneralItemMaster { get; set; }
        public DbSet<InventoryGeneralItemLine> InventoryGeneralItemLine { get; set; }
        public DbSet<InventoryItemModelGroup> InventoryItemModelGroup { get; set; }
        public DbSet<InventoryItemTrackingDimension> InventoryItemTrackingDimension { get; set; }
        public DbSet<InventoryProductDimension> InventoryProductDimension { get; set; }
        public DbSet<InventoryItemGroup> InventoryItemGroup { get; set; }
        public DbSet<InventoryProductDimensionGroup> InventoryProductDimensionGroup { get; set; }
        public DbSet<InventoryProductDimensionGroupMapper> InventoryProductDimensionGroupMapper { get; set; }
        public DbSet<InventoryUoMMaster> InventoryUoMMaster { get; set; }
        
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
