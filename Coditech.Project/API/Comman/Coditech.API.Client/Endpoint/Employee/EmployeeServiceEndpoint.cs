using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class EmployeeServiceEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechEmployeeApiRootUri}/EmployeeMaster/GetEmployeeList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string GetEmployeeServiceAsync(long employeeId) =>
            $"{CoditechAdminSettings.CoditechEmployeeApiRootUri}/EmployeeMaster/GetEmployeeService?employeeId={employeeId}";

        public string UpdateEmployeeServiceAsync() =>
               $"{CoditechAdminSettings.CoditechEmployeeApiRootUri}/EmployeeMaster/UpdateEmployeeService";

        public string DeleteEmployeeAsync() =>
                  $"{CoditechAdminSettings.CoditechEmployeeApiRootUri}/EmployeeMaster/DeleteEmployeeService";

        public string EmployeeServiceListAsync(long employeeId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechEmployeeApiRootUri}/EmployeeMaster/GetEmployeeList?employeeId={employeeId}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
    }
}
