using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralCurrencyMasterEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCurrencyMaster/GetCurrencyList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateCurrencyAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCurrencyMaster/CreateCurrency";

        public string GetCurrencyAsync(short generalCurrencyMasterId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCurrencyMaster/GetCurrency?generalCurrencyMasterId={generalCurrencyMasterId}";
       
        public string UpdateCurrencyAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCurrencyMaster/UpdateCurrency";

        public string DeleteCurrencyAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCurrencyMaster/DeleteCurrency";
    }
}
