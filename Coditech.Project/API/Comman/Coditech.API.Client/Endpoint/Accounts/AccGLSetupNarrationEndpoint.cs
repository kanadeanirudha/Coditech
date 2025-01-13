using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;
namespace Coditech.API.Endpoint
{
    public class AccGLSetupNarrationEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLSetupNarration/GetNarrationList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateNarrationAsync() => 
         $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLSetupNarration/CreateNarration";

        public string GetNarrationAsync(int  accGLSetupNarrationId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLSetupNarration/GetNarration?accGLSetupNarrationId={accGLSetupNarrationId}";

        public string UpdateNarrationAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLSetupNarration/UpdateNarration";

        public string DeleteNarrationAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLSetupNarration/DeleteNarration";
    }
}
