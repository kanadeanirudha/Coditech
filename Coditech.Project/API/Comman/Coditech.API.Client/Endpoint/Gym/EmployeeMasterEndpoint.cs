using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class EmployeeMasterEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/EmployeeMaster/GetEmployeeMasterList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateEmployeeAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/EmployeeMaster/CreateCountry";

        public string GetEmployeeAsync(int employeeId) =>
            $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/EmployeeMaster/GetEmployeeMaster?employeeId={employeeId}";

        public string UpdateEmployeeAsync() =>
               $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/EmployeeMaster/UpdateEmployeeMaster";

        public string DeleteEmployeeAsync() =>
                  $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/EmployeeMaster/DeleteEmployeeMaster";

        //public string GymMemberFollowUpListAsync(int employeeId,long personId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        //{
        //    string endpoint = $"{CoditechAdminSettings.CoditechGymManagementSystemApiRootUri}/EmployeeMaster/GymMemberFollowUpList?employeeId={employeeId}&personId={personId}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
        //    return endpoint;
        //}
    }
}
