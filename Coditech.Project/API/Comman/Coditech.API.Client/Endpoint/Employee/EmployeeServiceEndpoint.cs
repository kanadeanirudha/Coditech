using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class EmployeeServiceEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechEmployeeApiRootUri}/EmployeeService/GetEmployeeList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateEmployeeAsync() =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/EmployeeService/CreateEmployee";

        public string GetEmployeeServiceAsync(long employeeId) =>
            $"{CoditechAdminSettings.CoditechEmployeeApiRootUri}/EmployeeService/GetEmployeeService?employeeId={employeeId}";

        public string UpdateEmployeeServiceAsync() =>
               $"{CoditechAdminSettings.CoditechEmployeeApiRootUri}/EmployeeService/UpdateEmployeeService";

        public string DeleteEmployeeAsync() =>
                  $"{CoditechAdminSettings.CoditechEmployeeApiRootUri}/EmployeeService/DeleteEmployeeService";

        public string EmployeeServiceListAsync(long employeeId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechEmployeeApiRootUri}/EmployeeService/GetEmployeeList?employeeId={employeeId}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
    }
}
