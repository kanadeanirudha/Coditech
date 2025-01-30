using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;
namespace Coditech.API.Endpoint
{
    public class AccSetupTransactionTypeEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupTransactionType/GetTransactionTypeList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateTransactionTypeAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupTransactionType/CreateTransactionType";

        public string GetTransactionTypeAsync(short accSetupTransactionTypeId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupTransactionType/GetTransactionType?accSetupTransactionTypeId={accSetupTransactionTypeId}";

        public string UpdateTransactionTypeAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupTransactionType/UpdateTransactionType";

        public string DeleteTransactionTypeAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupTransactionType/DeleteTransactionType";
    }
}
