using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class AccGLOpeningBalanceEndpoint : BaseEndpoint
    {
        public string GetNonControlHeadTypeAsync(int accSetupBalanceSheetId, short accSetupCategoryId, byte controlNonControl) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLOpeningBalance/GetNonControlHeadType?accSetupBalanceSheetId={accSetupBalanceSheetId}&accSetupCategoryId={accSetupCategoryId}&controlNonControl={controlNonControl}{BuildEndpointQueryString(true)}";

        public string UpdateNonControlHeadTypeAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLOpeningBalance/UpdateNonControlHeadType";
    }
}
