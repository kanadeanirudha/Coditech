using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;
namespace Coditech.API.Endpoint
{
    public class AccSetupGLBankEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupGLBank/GetAccSetupGLBankList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateAccSetupGLBankAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupGLBank/CreateAccSetupGLBank";
        public string GetAccSetupGLBankAsync(int accSetupGLBankId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupGLBank/GetAccSetupGLBank?accSetupGLBankId={accSetupGLBankId}";
        public string UpdateAccSetupGLBankAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupGLBank/UpdateAccSetupGLBank";
        public string DeleteAccSetupGLBankAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupGLBank/DeleteAccSetupGLBank";
    }
}
