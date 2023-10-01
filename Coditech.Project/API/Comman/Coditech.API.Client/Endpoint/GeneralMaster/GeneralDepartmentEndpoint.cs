using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralDepartmentEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDepartmentMaster/GetDepartmentList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateDepartmentAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDepartmentMaster/CreateDepartment";

        public string GetDepartmentAsync(int generalDepartmentId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDepartmentMaster/GetDepartment?generalDepartmentId={generalDepartmentId}";
       
        public string UpdateDepartmentAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDepartmentMaster/UpdateDepartment";

        public string DeleteDepartmentAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDepartmentMaster/DeleteDepartment";

        public string GetDepartmentsByCentreCode(string centreCode)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralDepartmentMaster/GetDepartmentsByCentreCode?centreCode={centreCode}";
            return endpoint;
        }
    }
}
