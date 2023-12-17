using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Logger;

using Microsoft.Extensions.DependencyInjection;

using System.Collections.Generic;

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
    }
}