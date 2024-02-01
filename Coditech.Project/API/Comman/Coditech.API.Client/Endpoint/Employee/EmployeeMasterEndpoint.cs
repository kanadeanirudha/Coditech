using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class EmployeeMasterEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechEmployeeApiRootUri}/EmployeeMaster/GetEmployeeList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string GetEmployeeOtherDetailAsync(long employeeId) =>
            $"{CoditechAdminSettings.CoditechEmployeeApiRootUri}/EmployeeMaster/GetEmployeeOtherDetail?employeeId={employeeId}";

        public string UpdateEmployeeOtherDetailAsync() =>
               $"{CoditechAdminSettings.CoditechEmployeeApiRootUri}/EmployeeMaster/UpdateEmployeeOtherDetail";

        public string DeleteEmployeeAsync() =>
                  $"{CoditechAdminSettings.CoditechEmployeeApiRootUri}/EmployeeMaster/DeleteEmployeeMaster";
    }
}
