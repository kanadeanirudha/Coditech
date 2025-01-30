using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.API.Data;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class AccSetupBalanceSheetEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, byte accSetupBalanceSheetTypeId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupBalanceSheet/GetBalanceSheetList?selectedCentreCode={selectedCentreCode}&accSetupBalanceSheetTypeId={accSetupBalanceSheetTypeId}{BuildEndpointQueryString(true,expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateBalanceSheetAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupBalanceSheet/CreateBalanceSheet";

        public string GetBalanceSheetAsync(int accSetupBalanceSheetId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupBalanceSheet/GetBalanceSheet?accSetupBalanceSheetId={accSetupBalanceSheetId}";
       
        public string UpdateBalanceSheetAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupBalanceSheet/UpdateBalanceSheet";

        public string DeleteBalanceSheetAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupBalanceSheetMaster/DeleteBalanceSheet";

        public string GetBalanceSheetsByCentreCode(string centreCode)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupBalanceSheetMaster/GetBalanceSheetsByCentreCode?centreCode={centreCode}";
            return endpoint;
        }
    }
}
