using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
namespace Coditech.API.Endpoint
{
    public class AccGLSetupNarrationEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLSetupNarration/GetNarrationList?selectedCentreCode={selectedCentreCode}{BuildEndpointQueryString(true)}";
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
