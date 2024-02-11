using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralPersonFollowUpEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechUserApiRootUri}/GeneralPersonFollowUp/GetPersonFollowUpList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreatePersonFollowUpAsync() =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/GeneralPersonFollowUp/CreatePersonFollowUp";

        public string GetPersonFollowUpAsync(long generalPersonFollowUpId) =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/GeneralPersonFollowUp/GetPersonFollowUp?generalPersonFollowUpId={generalPersonFollowUpId}";
       
        public string UpdatePersonFollowUpAsync() =>
               $"{CoditechAdminSettings.CoditechUserApiRootUri}/GeneralPersonFollowUp/UpdatePersonFollowUp";

        public string DeletePersonFollowUpAsync() =>
                  $"{CoditechAdminSettings.CoditechUserApiRootUri}/GeneralPersonFollowUp/DeletePersonFollowUp";
    }
}
