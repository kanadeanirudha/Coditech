using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class EmployeeServiceEndpoint : BaseEndpoint
    {
        public string EmployeeServiceListAsync(long employeeId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/EmployeeService/GetEmployeeServiceList?employeeId={employeeId}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateEmployeeServiceAsync() =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/EmployeeService/CreateEmployeeService";

        public string GetEmployeeServiceAsync(long employeeId, long personId, long employeeServiceId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/EmployeeService/GetEmployeeService?employeeId={employeeId}&personId={personId}&employeeServiceId={employeeServiceId}";

        public string UpdateEmployeeServiceAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/EmployeeService/UpdateEmployeeService";

        public string DeleteEmployeeServiceAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/EmployeeService/DeleteEmployeeService";

    }
}
