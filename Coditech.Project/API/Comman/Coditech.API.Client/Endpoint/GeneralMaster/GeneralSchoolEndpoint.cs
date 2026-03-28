using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralSchoolEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralSchoolMaster/GetSchoolList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateSchoolAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralSchoolMaster/CreateSchool";

        public string GetSchoolAsync(short generalSchoolId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralSchoolMaster/GetSchool?generalSchoolMasterId={generalSchoolId}";
       
        public string UpdateSchoolAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralSchoolMaster/UpdateSchool";

        public string DeleteSchoolAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralSchoolMaster/DeleteSchool";
    }
}
