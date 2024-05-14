using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

using Microsoft.Extensions.DependencyInjection;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.Common.Service
{
    public abstract class BaseService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        public BaseService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = _serviceProvider.GetService<ICoditechLogging>();
        }
        protected virtual EmployeeDesignationMaster GetDesignationDetails(short designationId)
        {
            EmployeeDesignationMaster employeeDesignationMaster = new CoditechRepository<EmployeeDesignationMaster>(_serviceProvider.GetService<Coditech_Entities>()).GetById(designationId);
            return employeeDesignationMaster;
        }

        protected virtual GeneralDepartmentMaster GetDepartmentDetails(short departmentId)
        {
            GeneralDepartmentMaster generalDepartmentMaster = new CoditechRepository<GeneralDepartmentMaster>(_serviceProvider.GetService<Coditech_Entities>()).GetById(departmentId);
            return generalDepartmentMaster;
        }

        protected virtual OrganisationCentreMaster GetOrganisationCentreDetails(string centreCode)
        {
            OrganisationCentreMaster organisationCentreMaster = new CoditechRepository<OrganisationCentreMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.FirstOrDefault(x => x.CentreCode == centreCode);
            return organisationCentreMaster;
        }

        protected virtual List<UserAccessibleCentreModel> OrganisationCentreList()
        {
            List<OrganisationCentreMaster> centreList = new CoditechRepository<OrganisationCentreMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.ToList();
            List<UserAccessibleCentreModel> organisationCentreList = new List<UserAccessibleCentreModel>();
            foreach (OrganisationCentreMaster item in centreList)
            {
                organisationCentreList.Add(new UserAccessibleCentreModel()
                {
                    CentreCode = item.CentreCode,
                    CentreName = item.CentreName,
                    ScopeIdentity = "Centre"
                });
            }

            return organisationCentreList;
        }

        protected virtual List<UserModuleMaster> GetAllActiveModuleList()
        {
            List<UserModuleMaster> userAllModuleList = new CoditechRepository<UserModuleMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.ModuleActiveFlag == true)?.ToList();
            return userAllModuleList;
        }

        protected virtual List<UserMainMenuMaster> GetAllActiveMenuList(string moduleCode = null)
        {
            List<UserMainMenuMaster> userAllMenuList = new CoditechRepository<UserMainMenuMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.IsEnable == true && (x.ModuleCode == moduleCode || moduleCode == null))?.OrderBy(y => y.MenuDisplaySeqNo)?.ToList();
            return userAllMenuList;
        }

        protected virtual List<GeneralSystemGlobleSettingModel> GetSystemGlobleSettingList(string featureName = null)
        {
            List<GeneralSystemGlobleSettingMaster> settingList = new CoditechRepository<GeneralSystemGlobleSettingMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.FeatureName == featureName || featureName == null)?.ToList();
            List<GeneralSystemGlobleSettingModel> list = new List<GeneralSystemGlobleSettingModel>();
            foreach (GeneralSystemGlobleSettingMaster item in settingList)
            {
                list.Add(new GeneralSystemGlobleSettingModel()
                {
                    GeneralSystemGlobleSettingMasterId = item.GeneralSystemGlobleSettingMasterId,
                    FeatureName = item.FeatureName,
                    FeatureValue = item.FeatureValue
                });
            }
            return list;
        }

        protected virtual GeneralPersonModel GetGeneralPersonDetails(long personId)
        {
            GeneralPerson generalPerson = new CoditechRepository<GeneralPerson>(_serviceProvider.GetService<Coditech_Entities>()).GetById(personId);
            return generalPerson.FromEntityToModel<GeneralPersonModel>();
        }

        protected virtual GeneralPersonModel GetGeneralPersonDetailsByEntityType(long entityId, string entityType)
        {
            long personId = 0;
            string centreCode = string.Empty;
            string personCode = string.Empty;
            short generalDepartmentMasterId = 0;
            if (entityType == UserTypeEnum.GymMember.ToString())
            {
                GymMemberDetails gymMemberDetails = new CoditechRepository<GymMemberDetails>(_serviceProvider.GetService<Coditech_Entities>()).Table.FirstOrDefault(x => x.GymMemberDetailId == entityId);
                if (IsNotNull(gymMemberDetails))
                {
                    personId = gymMemberDetails.PersonId;
                    centreCode = gymMemberDetails.CentreCode;
                }
            }
            else if (entityType == UserTypeEnum.Employee.ToString())
            {
                EmployeeMaster employeeMaster = new CoditechRepository<EmployeeMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.FirstOrDefault(x => x.EmployeeId == entityId);
                if (IsNotNull(employeeMaster))
                {
                    personId = employeeMaster.PersonId;
                    centreCode = employeeMaster.CentreCode;
                    personCode = employeeMaster.PersonCode;
                    generalDepartmentMasterId = employeeMaster.GeneralDepartmentMasterId;
                }
            }
            GeneralPersonModel generalPersonModel = GetGeneralPersonDetails(personId);
            if (IsNotNull(generalPersonModel))
            {
                generalPersonModel.SelectedCentreCode = centreCode;
                generalPersonModel.SelectedDepartmentId = Convert.ToString(generalDepartmentMasterId);
                generalPersonModel.PersonCode = personCode;
            }
            return generalPersonModel;
        }

        protected virtual string GetEnumCodeByEnumId(int generalEnumaratorId)
        {
            if (generalEnumaratorId == 0)
                return string.Empty;

            GeneralEnumaratorMaster generalEnumaratorMaster = new CoditechRepository<GeneralEnumaratorMaster>(_serviceProvider.GetService<Coditech_Entities>()).GetById(generalEnumaratorId);
            return generalEnumaratorMaster.EnumName;
        }

        protected virtual string GetEnumDisplayTextByEnumId(int generalEnumaratorId)
        {
            if (generalEnumaratorId == 0)
                return string.Empty;

            GeneralEnumaratorMaster generalEnumaratorMaster = new CoditechRepository<GeneralEnumaratorMaster>(_serviceProvider.GetService<Coditech_Entities>()).GetById(generalEnumaratorId);
            return generalEnumaratorMaster.EnumDisplayText;
        }

        protected virtual int GetEnumIdByEnumCode(string enumCode)
        {
            GeneralEnumaratorMaster generalEnumaratorMaster = new CoditechRepository<GeneralEnumaratorMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.FirstOrDefault(x => x.EnumName == enumCode);
            return generalEnumaratorMaster.GeneralEnumaratorId;
        }

        protected virtual string GenerateRegistrationCode(string enumCode, string centreCode)
        {
            string registrationCode = string.Empty;
            int generalEnumaratorId = GetEnumIdByEnumCode(enumCode);
            CoditechRepository<GeneralRunningNumbers> _generalRunningNumbersRepository = new CoditechRepository<GeneralRunningNumbers>(_serviceProvider.GetService<Coditech_Entities>());
            GeneralRunningNumbers generalRunningNumbers = _generalRunningNumbersRepository.Table.FirstOrDefault(x => x.KeyFieldEnumId == generalEnumaratorId && x.IsActive && !x.IsRowLock && x.CentreCode == centreCode);
            if (generalRunningNumbers != null)
            {
                DateTime dateTime = DateTime.Now;
                generalRunningNumbers.CurrentSequnce = (generalRunningNumbers.CurrentSequnce + 1);
                registrationCode = generalRunningNumbers.DisplayFormat?.ToLower();
                registrationCode = registrationCode.Replace("<centrecode>", centreCode);
                registrationCode = registrationCode.Replace("<separator>", generalRunningNumbers.Separator);
                registrationCode = registrationCode.Replace("<prefix>", generalRunningNumbers.Prefix);
                registrationCode = registrationCode.Replace("<yyyy>", dateTime.Year.ToString());
                registrationCode = registrationCode.Replace("<yy>", dateTime.Year.ToString());
                registrationCode = registrationCode.Replace("<mm>", dateTime.Month.ToString());
                registrationCode = registrationCode.Replace("<dd>", dateTime.Date.ToString());
                registrationCode = registrationCode.Replace("<hh>", dateTime.Hour.ToString());
                registrationCode = registrationCode.Replace("<mm>", dateTime.Minute.ToString());
                registrationCode = registrationCode.Replace("<sec>", dateTime.Second.ToString());
                registrationCode = registrationCode.Replace("<currentsequence>", (generalRunningNumbers.CurrentSequnce).ToString());
                _generalRunningNumbersRepository.Update(generalRunningNumbers);
            }
            return registrationCode;
        }

        protected virtual InventoryGeneralItemLineDetailsModel GetInventoryGeneralItemLineDetails(long inventoryGeneralItemLineId)
        {
            CoditechRepository<InventoryGeneralItemMaster> _inventoryGeneralItemMasterRepository = new CoditechRepository<InventoryGeneralItemMaster>(_serviceProvider.GetService<Coditech_Entities>());
            CoditechRepository<InventoryGeneralItemLine> _inventoryGeneralItemLineRepository = new CoditechRepository<InventoryGeneralItemLine>(_serviceProvider.GetService<Coditech_Entities>());

            InventoryGeneralItemLineDetailsModel itemLineDetails = null;
            itemLineDetails = (from a in _inventoryGeneralItemMasterRepository.Table
                               join b in _inventoryGeneralItemLineRepository.Table
                               on a.InventoryGeneralItemMasterId equals b.InventoryGeneralItemMasterId
                               where (b.InventoryGeneralItemLineId == inventoryGeneralItemLineId)
                               select new InventoryGeneralItemLineDetailsModel()
                               {
                                   InventoryGeneralItemMasterId = a.InventoryGeneralItemMasterId,
                                   InventoryGeneralItemLineId = b.InventoryGeneralItemLineId,
                                   ItemNumber = a.ItemNumber,
                                   ItemDescription = a.ItemDescription,
                                   GeneralTaxGroupMasterId = a.GeneralTaxGroupMasterId,
                                   HSNSACCode = a.HSNSACCode,
                                   ItemName = b.ItemName,
                                   SKU = b.SKU,

                               })?.FirstOrDefault();
            return itemLineDetails;
        }
        protected virtual decimal ItemLineTaxAmount(byte generalTaxGroupMasterId, decimal amount)
        {
            CoditechRepository<GeneralTaxGroupMasterDetails> _generalTaxGroupMasterDetailsRepository = new CoditechRepository<GeneralTaxGroupMasterDetails>(_serviceProvider.GetService<Coditech_Entities>());
            CoditechRepository<GeneralTaxMaster> _generalTaxMasterRepository = new CoditechRepository<GeneralTaxMaster>(_serviceProvider.GetService<Coditech_Entities>());
            decimal? taxInPercentage = (from a in _generalTaxGroupMasterDetailsRepository.Table
                                        join b in _generalTaxMasterRepository.Table
                                        on a.GeneralTaxMasterId equals b.GeneralTaxMasterId
                                        where (a.GeneralTaxGroupMasterId == generalTaxGroupMasterId)
                                        select b.TaxRate).Sum();

            return Convert.ToDecimal(amount * (taxInPercentage / 100));
        }

        protected virtual void ActiveInActiveUserLogin(bool flag, long entityId, string userType)
        {
            CoditechRepository<UserMaster> _userMasterRepository = new CoditechRepository<UserMaster>(_serviceProvider.GetService<Coditech_Entities>());
            UserMaster userMaster = _userMasterRepository.Table.Where(x => x.EntityId == entityId && x.UserType == userType)?.FirstOrDefault();
            if (userMaster != null && userMaster.IsActive != flag)
            {
                userMaster.IsActive = flag;
                _userMasterRepository.Update(userMaster);
            }
        }

        protected virtual short GetOrganisationCentreMasterIdByCentreCode(string centreCode)
        {
            CoditechRepository<OrganisationCentreMaster> _organisationCentreMasterRepository = new CoditechRepository<OrganisationCentreMaster>(_serviceProvider.GetService<Coditech_Entities>());
            short organisationCentreMasterId = _organisationCentreMasterRepository.Table.Where(x => x.CentreCode == centreCode).Select(y => y.OrganisationCentreMasterId).FirstOrDefault();
            return organisationCentreMasterId;
        }

        protected virtual string GetOrganisationCentreCodeByOrganisationCentreMasterId(short organisationCentreMasterId)
        {
            CoditechRepository<OrganisationCentreMaster> _organisationCentreMasterRepository = new CoditechRepository<OrganisationCentreMaster>(_serviceProvider.GetService<Coditech_Entities>());
            string CentreCode = _organisationCentreMasterRepository.Table.Where(x => x.OrganisationCentreMasterId == organisationCentreMasterId).Select(y => y.CentreCode).FirstOrDefault();
            return CentreCode;
        }

        protected virtual GeneralEmailTemplateModel GetEmailTemplateByCode(string centreCode, string emailTemplateByCode)
        {
            GeneralEmailTemplateModel emailTemplateModel = new GeneralEmailTemplateModel();
            if (!string.IsNullOrEmpty(centreCode))
            {
                OrganisationCentrewiseEmailTemplate organisationCentrewiseEmailTemplate = new CoditechRepository<OrganisationCentrewiseEmailTemplate>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.CentreCode == centreCode && x.EmailTemplateCode == emailTemplateByCode)?.FirstOrDefault();
                if (IsNotNull(organisationCentrewiseEmailTemplate)) {
                    emailTemplateModel.EmailTemplateCode = organisationCentrewiseEmailTemplate.EmailTemplateCode;
                    emailTemplateModel.EmailTemplate = organisationCentrewiseEmailTemplate.EmailTemplate;
                    emailTemplateModel.Subject = organisationCentrewiseEmailTemplate.Subject;
                }
            }
            else
            {
                GeneralEmailTemplate generalEmailTemplate = new CoditechRepository<GeneralEmailTemplate>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.EmailTemplateCode == emailTemplateByCode)?.FirstOrDefault();
                if (IsNotNull(generalEmailTemplate))
                {
                    emailTemplateModel.EmailTemplateCode = generalEmailTemplate.EmailTemplateCode;
                    emailTemplateModel.EmailTemplate = generalEmailTemplate.EmailTemplate;
                    emailTemplateModel.Subject = generalEmailTemplate.Subject;
                }
            }
            return emailTemplateModel;
        }
    }
}