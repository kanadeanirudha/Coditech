using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class EmployeeMasterEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/EmployeeMaster/GetEmployeeList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string ListByCentreCodeAsync(string centreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/EmployeeMaster/GetEmployeeListByCentreCode?centreCode={centreCode}&{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string GetEmployeeOtherDetailAsync(long employeeId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/EmployeeMaster/GetEmployeeOtherDetail?employeeId={employeeId}";

        public string UpdateEmployeeOtherDetailAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/EmployeeMaster/UpdateEmployeeOtherDetail";

        public string DeleteEmployeeAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/EmployeeMaster/DeleteEmployeeMaster";
    }
}
