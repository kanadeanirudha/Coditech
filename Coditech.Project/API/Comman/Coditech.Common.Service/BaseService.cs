using Coditech.API.Data;
using Coditech.Common.API.Model;

namespace Coditech.Common.Service
{
    public abstract class BaseService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected EmployeeDesignationMaster GetDesignationDetails(short designationId)
        {
            EmployeeDesignationMaster employeeDesignationMaster = new CoditechRepository<EmployeeDesignationMaster>().GetById(designationId);
            return employeeDesignationMaster;
        }

        protected GeneralDepartmentMaster GetDepartmentDetails(short departmentId)
        {
            GeneralDepartmentMaster generalDepartmentMaster = new CoditechRepository<GeneralDepartmentMaster>().GetById(departmentId);
            return generalDepartmentMaster;
        }

        protected OrganisationCentreMaster GetOrganisationCentreDetails(string centreCode)
        {
            OrganisationCentreMaster organisationCentreMaster = new CoditechRepository<OrganisationCentreMaster>().Table.FirstOrDefault(x => x.CentreCode == centreCode);
            return organisationCentreMaster;
        }

        protected List<UserAccessibleCentreModel> OrganisationCentreList()
        {
            List<OrganisationCentreMaster> centreList = new CoditechRepository<OrganisationCentreMaster>().Table.ToList();
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
    }
}