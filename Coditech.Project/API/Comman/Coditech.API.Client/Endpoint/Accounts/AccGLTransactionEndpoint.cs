using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.API.Data;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class AccGLTransactionEndpoint : BaseEndpoint
    {
        public string ListAsync( string selectedCentreCode, int accSetupBalanceSheetId, short generalFinancialYearId, short accSetupTransactionTypeId, byte accSetupBalanceSheetTypeId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLTransaction/GetGLTransactionList?selectedCentreCode={selectedCentreCode}&accSetupBalanceSheetId={accSetupBalanceSheetId}&generalFinancialYearId={generalFinancialYearId}&accSetupTransactionTypeId={accSetupTransactionTypeId}&accSetupBalanceSheetTypeId={accSetupBalanceSheetTypeId}{BuildEndpointQueryString(true,expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateGLTransactionAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLTransaction/CreateGLTransaction";

        public string GetGLTransactionAsync(long accGLTransactionId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLTransaction/GetGLTransaction?accGLTransactionId={accGLTransactionId}";
       
        public string UpdateGLTransactionAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLTransaction/UpdateGLTransaction";

        //public string DeleteBalanceSheetAsync() =>
        //          $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLTransactionMaster/DeleteBalanceSheet";

        //public string GetGLTransactionByCentreCode(string centreCode)
        //{
        //    string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLTransactionMaster/GetBalanceSheetsByCentreCode?centreCode={centreCode}";
        //    return endpoint;
        //}
    }
}
