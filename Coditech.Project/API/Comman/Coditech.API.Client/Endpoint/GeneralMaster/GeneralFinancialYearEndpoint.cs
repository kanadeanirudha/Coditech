using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralFinancialYearEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralFinancialYearMaster/GetFinancialYearList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateFinancialYearAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralFinancialYearMaster/CreateFinancialYear";

        public string GetFinancialYearAsync(short generalFinancialYearId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralFinancialYearMaster/GetFinancialYear?generalFinancialYearMasterId={generalFinancialYearId}";

        public string UpdateFinancialYearAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralFinancialYearMaster/UpdateFinancialYear";

        public string DeleteFinancialYearAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralFinancialYearMaster/DeleteFinancialYear";

        public string GetCurrentFinancialYearAsync(int accSetupBalanceSheetId) =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralFinancialYearMaster/GetCurrentFinancialYear?accSetupBalanceSheetId={accSetupBalanceSheetId}";
    }
}
