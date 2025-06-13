using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralBatchEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, long userId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralBatchMaster/GetBatchList?selectedCentreCode={selectedCentreCode}&userId={userId}{BuildEndpointQueryString(true,expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateGeneralBatchAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralBatchMaster/CreateGeneralBatch";

        public string GetGeneralBatchAsync(int generalBatchMasterId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralBatchMaster/GetGeneralBatch?generalBatchMasterId={generalBatchMasterId}";

        public string UpdateGeneralBatchAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralBatchMaster/UpdateGeneralBatch";

        public string DeleteGeneralBatchAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralBatchMaster/DeleteGeneralBatch";

        public string GeneralBatchUserListAsync(int generalBatchMasterId, string userType, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralBatchMaster/GetGeneralBatchUserList?generalBatchMasterId={generalBatchMasterId}&userType={userType}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string AssociateUnAssociateBatchwiseUserAsync() =>
       $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralBatchMaster/AssociateUnAssociateBatchwiseUser";

    }
}
