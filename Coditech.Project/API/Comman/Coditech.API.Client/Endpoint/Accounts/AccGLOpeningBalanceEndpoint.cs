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

        public string GetControlHeadTypeAsync(int accSetupBalanceSheetId, short accSetupCategoryId, byte controlNonControl) =>
             $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLOpeningBalance/GetControlHeadType?accSetupBalanceSheetId={accSetupBalanceSheetId}&accSetupCategoryId={accSetupCategoryId}&controlNonControl={controlNonControl}{BuildEndpointQueryString(true)}";

        public string GetIndividualOpeningBalanceAsync(int accSetupBalanceSheetId, short userTypeId, short generalFinancialYearId, int accSetupGLId) =>
              $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLOpeningBalance/GetIndividualOpeningBalance?accSetupBalanceSheetId={accSetupBalanceSheetId}&userTypeId={userTypeId}&generalFinancialYearId={generalFinancialYearId}&accSetupGLId{accSetupGLId}{BuildEndpointQueryString(true)}";
        public string UpdateIndividualOpeningBalanceAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLOpeningBalance/UpdateIndividualOpeningBalance";
    }
}
