using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Enum;
using Coditech.Common.Logger;

using Microsoft.Extensions.DependencyInjection;

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

        protected virtual List<GeneralSystemGlobleSettingMaster> GetSystemGlobleSettingList(string featureName = null)
        {
            List<GeneralSystemGlobleSettingMaster> settingList = new CoditechRepository<GeneralSystemGlobleSettingMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.FeatureName == featureName || featureName == null)?.ToList();
            return settingList;
        }

        protected virtual GeneralPerson GetGeneralPersonDetails(long personId)
        {
            GeneralPerson generalPerson = new CoditechRepository<GeneralPerson>(_serviceProvider.GetService<Coditech_Entities>()).GetById(personId);
            return generalPerson;
        }

        protected virtual GeneralPerson GetGeneralPersonDetailsByEntityType(long entityId, string entityType)
        {
            long personId = 0;
            if (entityType == UserTypeEnum.GymMember.ToString())
            {
                personId = new CoditechRepository<GymMemberDetails>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.GymMemberDetailId == entityId).FirstOrDefault().PersonId;
            }
            return GetGeneralPersonDetails(personId);
        }

        protected virtual string GetEnumCodeByEnumId(int generalEnumaratorId)
        {
            if (generalEnumaratorId == 0)
                return string.Empty;

            GeneralEnumaratorMaster generalEnumaratorMaster = new CoditechRepository<GeneralEnumaratorMaster>(_serviceProvider.GetService<Coditech_Entities>()).GetById(generalEnumaratorId);
            return generalEnumaratorMaster.EnumName;
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

    }
}